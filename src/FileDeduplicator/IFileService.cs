namespace FileDeduplicator
{
    public interface IFileService
    {
        /// <summary>
        /// Groups the files in the specified directory by their size.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to scan for files.</param>
        /// <returns>A dictionary where the key is the file size (in bytes) and the value is a list of file paths with that size.</returns>
        Dictionary<long, List<string>> GroupFilesBySize(string directoryPath);
    }
}