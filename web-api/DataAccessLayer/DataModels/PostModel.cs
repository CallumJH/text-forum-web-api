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

    public Post MapToPost(int likes)
    {
        return new Post
        {
            Title = Title,
            Likes = likes,
            // Comments = new List<Comment>() TODO: Connect comments to posts
        };
    }
    
}