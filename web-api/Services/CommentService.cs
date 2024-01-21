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
            return new RequestWrapper<List<Comment>>();
        }
    }

    /// <summary>
    /// This function is used to create a comment.
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="userModel"></param>
    /// <returns></returns>
    public async Task<RequestWrapper> CreateComment(Comment comment, UserModel userModel)
    {
        try 
        {
            var request = new RequestWrapper();
            var postModel = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == comment.PostId);
            if (postModel == null)
            {
                request.Message = "Post does not exist";
                return request;
            }
            var commentModel = new CommentModel()
            {
                Content = comment.Content,
                PostId = comment.PostId,
                UserId = userModel.Id,
            };
            await connection.InsertAsync(commentModel);
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper();
        }
    }

    /// <summary>
    /// This function is used to like a comment.
    /// </summary>
    /// <param name="id">
    /// The id of the comment to like.
    /// </param>
    /// <param name="userModel">
    /// The user model of the user liking the comment.
    /// </param>
    /// <returns>
    /// A request wrapper containing the result of the like.
    /// </returns>
    public async Task<RequestWrapper> LikeComment(int id, UserModel userModel)
    {
        try 
        {
            var request = new RequestWrapper();
            var commentModel = await connection.GetTable<CommentModel>().FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                request.Message = "Comment does not exist";
                return request;
            }
            var commentLikeModel = await connection.GetTable<LikeModel>().FirstOrDefaultAsync(x => x.CommentId == id && x.UserId == userModel.Id);
            if (commentLikeModel != null)
            {
                request.Message = "Comment already liked";
                return request;
            }
            commentLikeModel = new LikeModel()
            {
                CommentId = id,
                UserId = userModel.Id,
            };
            commentModel.LikeCount++;
            await connection.InsertAsync(commentLikeModel);
            await connection.UpdateAsync(commentModel);
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper();
        }
    }
}