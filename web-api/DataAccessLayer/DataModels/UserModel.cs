using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "Users")]
public class UserModel : BaseTableData 
{
    [Column, NotNull]
    public string Username { get; set; }
    [Column, NotNull]
    public string Salt { get; set; }

    [Column, NotNull]
    public string Password { get; set; }

    [Column, NotNull]
    public int IsModerator { get; set; } = 0;
}