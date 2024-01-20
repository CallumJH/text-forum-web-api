using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "Posts")]
public class PostModel : CommonPostData 
{
    [Column, NotNull]
    public string Title { get; set; }
    
    [Column, NotNull]
    [Association(ThisKey = "UserId", OtherKey = "Id")]
    public int UserId { get; set; }
    
}