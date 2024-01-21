using LinqToDB.Mapping;

[Table(Name = "Sessions")]
public class SessionModel
{
    [Column, NotNull, PrimaryKey, Identity]
    public int UserId { get; set; }

    
    [Column, NotNull]
    public DateTime ExpiresAt { get; set; }

    [Column, NotNull]
    public string Token { get; set; }
}