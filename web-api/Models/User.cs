using System.ComponentModel.DataAnnotations;

/// <summary>
/// User model for login and signup.
/// [Required] attribute is used to validate the model specifying required fields.
/// </summary>
public class User
{
    /// <summary>
    /// Username must be 3-20 characters long and contain only letters, numbers, underscores and hyphens.
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$")]
    public string Username { get; set; }
    
    /// <summary>
    /// Password must be 8-20 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character.
    /// </summary>
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$")]
    public string Password { get; set; }
}