using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "UserPosts")]
public class UserPostModel : BaseTableData 
{

    [Column, NotNull]
    public int IsOwnPost { get; set; } = 0;
    [Column, NotNull]
    public int IsLiked { get; set; } = 0;
}