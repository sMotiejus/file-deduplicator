using System.Security.Cryptography;

namespace FileDeduplicator;

/// <summary>
/// Provides hashing functionality using the MD5 algorithm.
/// </summary>
public class Md5HashingService : IHashingService
{
    /// <summary>
    /// Computes the MD5 hash of a file and returns it as a lowercase hex string.
    /// </summary>
    /// <param name="filePath">The path of the file to hash.</param>
    /// <returns>A lowercase hexadecimal representation of the MD5 hash.</returns>
    public string GetFileHash(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        using var md5 = MD5.Create();
        
        return Convert.ToHexStringLower(md5.ComputeHash(stream));
    }
}