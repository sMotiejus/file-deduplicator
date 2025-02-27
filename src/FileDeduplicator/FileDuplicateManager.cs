using System.Collections.Concurrent;

namespace FileDeduplicator;

/// <summary>
/// Manages file deduplication by grouping files of the same size 
/// and then comparing their hashes to find duplicates.
/// </summary>
public class FileDuplicatorManager(IHashingService hashingService, IFileService fileService)
{
    /// <summary>
    /// Initializes a new instance of FileDuplicatorManager 
    /// with MD5 as the default hashing algorithm and a default file service.
    /// </summary>
    public FileDuplicatorManager() : this(new Md5HashingService(), new FileService()) {}

    /// <summary>
    /// Initializes a new instance of FileDuplicatorManager
    /// with a custom hashing service and a default file service.
    /// </summary>
    /// <param name="hashingService">The hashing service to use for file comparison.</param>
    public FileDuplicatorManager(IHashingService hashingService) : this(hashingService, new FileService()) {}

    /// <summary>
    /// Initializes a new instance of FileDuplicatorManager 
    /// with a custom file service and MD5 as the default hashing algorithm.
    /// </summary>
    /// <param name="fileService">The file service to use for file operations.</param>
    public FileDuplicatorManager(IFileService fileService) : this(new Md5HashingService(), fileService) {}

    /// <summary>
    /// Finds duplicate files within the specified directory by first grouping them 
    /// by size and then computing their hash values for further comparison.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to scan for duplicate files.</param>
    /// <returns>A dictionary where the keys are file hash values, 
    /// and the values are lists of duplicate file paths.</returns>
    public Dictionary<string, List<string>> FindDuplicates(string directoryPath)
    {
        var sizeGroups = fileService.GroupFilesBySize(directoryPath);
        var duplicates = new ConcurrentDictionary<string, List<string>>();
        
        Parallel.ForEach(sizeGroups.Where(g => g.Value.Count > 1), group =>
        {
            var fileHashes = new ConcurrentDictionary<string, string>();
            
            Parallel.ForEach(group.Value, file =>
            {
                var hash = string.Intern(hashingService.GetFileHash(file));

                fileHashes.AddOrUpdate(hash, file, (_, existingFile) =>
                {
                    duplicates.AddOrUpdate(hash, _ => [existingFile, file], (_, list) =>
                    {
                        lock (list) { list.Add(file); }
                        return list;
                    });

                    return existingFile;
                });
            });
        });

        return duplicates.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
