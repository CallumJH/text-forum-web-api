using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;

public class CommentService : ICommentService
{
    private readonly DataBaseConnection connection;

    public CommentService(DataBaseConnection connection)
    {
        this.connection = connection;
    }

    public async Task<RequestWrapper<List<Comment>>> GetComments(int postId)
    {
        try 
        {
            var request = new RequestWrapper<List<Comment>>();
            var comments = await connection.GetTable<CommentModel>().Where(x => x.PostId == postId).ToListAsync();
            request.Data = comments.Select(x => x.MapToComment()).ToList();
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper<List<Comment>>();
            return request;
        }
    }

    public async Task<RequestWrapper> CreateComment(Comment comment)
    {
        try 
        {
            var request = new RequestWrapper();
            var commentModel = new CommentModel()
            {
                Content = comment.Content,
                PostId = 1, //TODO: Change this to the actual post id
                UserId = 1, //TODO: Change this to the actual user id
            };
            await connection.InsertAsync(commentModel);
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper();
            return request;
        }
    }

    public async Task<RequestWrapper> LikeComment(int id)
    {
        var request = new RequestWrapper();
        return request;
    }
}