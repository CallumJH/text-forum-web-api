using System.ComponentModel.DataAnnotations;

/// <summary>
/// Comment model for creating comments this is not to be confused with the comment DataModel. 
/// </summary>
public class Comment{
    /// <summary>
    /// Content must be 3-100 characters long and contain only letters, numbers, underscores and hyphens.
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_-]{3,100}$")]
    public string Content { get; set; }

    /// <summary>
    /// PostId is the id of the post the comment is being created for.
    /// </summary>
    [Required]
    public int PostId { get; set; }

    /// <summary>
    /// DateCreated is set to the current date and time when the comment is created keeping all values to UTC.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The likes count for the comment.
    /// </summary>
    public int Likes { get; set; }
}