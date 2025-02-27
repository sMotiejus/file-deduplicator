using FileDeduplicator;

namespace ConsoleExample;

internal abstract class Program
{
    private static void Main()
    {
        // Define the folder path to scan for duplicate files
        const string folderPath = "D:/test";
        Console.WriteLine($"Scanning for duplicate files in: {folderPath}");

        // Create an instance of the file duplicate manager (defaulting to MD5 hashing)
        var duplicateFileManager = new FileDuplicatorManager();

        // Find duplicate files in the specified folder
        var duplicates = duplicateFileManager.FindDuplicates(folderPath);

        // Display results
        DisplayResults(duplicates);
    }

    /// <summary>
    /// Displays the duplicate files found, if any.
    /// </summary>
    /// <param name="duplicates">A dictionary of duplicate file hashes and their corresponding file paths.</param>
    private static void DisplayResults(Dictionary<string, List<string>> duplicates)
    {
        if (duplicates.Count == 0)
        {
            Console.WriteLine("No duplicate files found.");
            return;
        }

        Console.WriteLine("\nDuplicate files detected:");

        foreach (var duplicate in duplicates)
        {
            Console.WriteLine($"\nHash: {duplicate.Key}");
            foreach (var file in duplicate.Value)
            {
                Console.WriteLine($"   {file}");
            }
        }
    }
}