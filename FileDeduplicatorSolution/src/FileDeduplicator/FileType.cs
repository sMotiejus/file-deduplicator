namespace FileDeduplicator;

/// <summary>
/// Represents the various file types that can be filtered during file deduplication.
/// </summary>
public enum FileType
{
    /// <summary>
    /// Check all file types without any filtering.
    /// </summary>
    ALL,   
    
    /// <summary>
    /// Check only PNG files.
    /// </summary>
    PNG,      
    
    /// <summary>
    /// Check only JPG files.
    /// </summary>
    JPG, 
    
    /// <summary>
    /// Check only JPEG files.
    /// </summary>
    JPEG,  
    
    /// <summary>
    /// Check only GIF files.
    /// </summary>
    GIF,    
    
    /// <summary>
    /// Check only MP4 files.
    /// </summary>
    MP4,    
    
    /// <summary>
    /// Check only MKV files.
    /// </summary>
    MKV,     
    
    /// <summary>
    /// Check only AVI files.
    /// </summary>
    AVI,     
    
    /// <summary>
    /// Check only TXT files.
    /// </summary>
    TXT,  
    
    /// <summary>
    /// Check only PDF files.
    /// </summary>
    PDF,
    
    
    /// <summary>
    /// Check only MOV files.
    /// </summary>
    MOV,
    
    /// <summary>
    /// Check only WMV files.
    /// </summary>
    WMV,
    
    /// <summary>
    /// Check only AVCHD files.
    /// </summary>
    AVCHD,
    
    /// <summary>
    /// Check only WebM files.
    /// </summary>
    WEBM,
    
    /// <summary>
    /// Check only FLV files.
    /// </summary>
    FLV,
}