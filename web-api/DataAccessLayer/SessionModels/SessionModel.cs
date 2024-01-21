using LinqToDB.Mapping;

[Table(Name = "Sessions")]
public class SessionModel
{
    public SessionModel(int userId, string token)
    {
        UserId = userId;
        Token = token;
    }
    [Column, NotNull, PrimaryKey, Identity]
    public int UserId { get; set; }

    
    [Column, NotNull]
    public DateTime ExpiresAt { get; set; } = DateTime.Now.AddHours(1);

    [Column, NotNull]
    public string Token { get; set; }
}