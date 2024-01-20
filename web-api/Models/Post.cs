using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post model for creating posts this is not to be confused with the POCO model.
/// </summary>
public class Post
{   
    /// <summary>
    /// Title must be 3-20 characters long and contain only letters, numbers, underscores and hyphens.
    /// </summary>
    [Required] 
    [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$")]
    public string Title { get; set; }

    // Max string length for sqlite is 4000 characters, given enough space for the content to 
    // contain decent sizeable information.
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_-]{3,3000}$")]
    public string Content { get; set; }
    
    /// <summary>
    /// DateCreated is set to the current date and time when the post is created keeping all values to UTC.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}