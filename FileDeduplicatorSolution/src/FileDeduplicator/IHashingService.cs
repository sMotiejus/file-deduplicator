namespace FileDeduplicator;

public interface  IHashingService
{
    /// <summary>
    /// Computes hash of a file and returns it as a lowercase hex string.
    /// </summary>
    /// <param name="filePath">The path of the file to hash.</param>
    /// <returns>A lowercase hexadecimal representation of the hash</returns>
    string GetFileHash(string filePath);
}