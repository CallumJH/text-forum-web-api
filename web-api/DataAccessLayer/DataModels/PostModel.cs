using LinqToDB.Mapping;

namespace DataModels;

[Table(Name = "Posts")]
public class PostModel : CommonPostData 
{
    public PostModel(string title, string content, int createdByUserID) : base(content, createdByUserID)
    {
        Title = title;
    }

    public PostModel()
    {
        
    }

    [Column, NotNull]
    public string Title { get; set; }
    

    /// <summary>
    /// Maps the PostModel to a Post object to return to user.
    /// </summary>
    /// <returns>
    /// Post object.
    /// </returns>
    public Post MapToPost()
    {
        return new Post
        {
            Title = Title,
            Content = Content,
            Likes = LikeCount
        };
    }
    
}