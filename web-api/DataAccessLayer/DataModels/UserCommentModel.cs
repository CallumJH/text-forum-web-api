using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "UserComments")]
public class UserCommentModel : BaseTableData 
{

    [Column, NotNull]
    public int IsOwnPost { get; set; } = 0;
    [Column, NotNull]
    public int IsLiked { get; set; } = 0;
}