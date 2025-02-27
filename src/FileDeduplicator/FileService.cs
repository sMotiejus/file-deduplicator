using System.Collections.Concurrent;

namespace FileDeduplicator;

public class FileService : IFileService
{
    // File type ( default - ALL )
    private readonly string _fileType;
        
    /// <summary>
    /// Initializes a new instance of the FileService class with the default file type, which is FileType.All.
    /// </summary>
    public FileService()
    {
        _fileType = FileType.ALL.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the FileService class with the specified file type.
    /// </summary>
    /// <param name="fileType">The file type as an enum (e.g., "FileType.PNG", "FileType.JPG").</param>
    public FileService(FileType fileType)
    {
        _fileType = fileType.ToString();
    }
        
    /// <summary>
    /// Initializes a new instance of the FileService class with the specified file type.
    /// </summary>
    /// <param name="fileType">The file type as a string (e.g., "PNG", "JPG", "TXT").</param>
    public FileService(string fileType)
    {
        _fileType = fileType.ToUpperInvariant();
    }

    /// <summary>
    /// Groups the files in the specified directory by their size.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to scan for files.</param>
    /// <returns>A dictionary where the key is the file size (in bytes) and the value is a list of file paths with that size.</returns>
    public Dictionary<long, List<string>> GroupFilesBySize(string directoryPath)
    {
        var sizeGroups = new ConcurrentDictionary<long, List<string>>();

        Parallel.ForEach(
            Directory.EnumerateFiles(
                directoryPath, "*.*", SearchOption.AllDirectories), file =>
        {
            if (_fileType != FileType.ALL.ToString() && !file.EndsWith(_fileType, StringComparison.OrdinalIgnoreCase))
                return;

            var fileSize = new FileInfo(file).Length;

            sizeGroups.AddOrUpdate(fileSize, _ => [file], (_, list) =>
            {
                lock (list) { list.Add(file); }
                return list;
            });
        });

        return sizeGroups.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}