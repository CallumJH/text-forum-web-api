using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "Comments")]
public class CommentModel : CommonPostData 
{
    [Column, NotNull]
    [Association(ThisKey = "PostId", OtherKey = "Id")]
    public int PostId { get; set; }
    
    [Column, NotNull]
    [Association(ThisKey = "UserId", OtherKey = "Id")]
    public int UserId { get; set; }

    public Comment MapToComment()
    {
        return new Comment
        {
            DateCreated = DateCreated,
            Content = Content,
            Likes = LikeCount
        };
    }
}