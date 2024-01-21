using DataAccessLayer;
using Interfaces;

public class CommentService : ICommentService
{
    private readonly DataBaseConnection _connection;

    public CommentService(DataBaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<RequestWrapper<List<Comment>>> GetComments(int postId)
    {
        var request = new RequestWrapper<List<Comment>>();
        return request;
    }

    public async Task<RequestWrapper> CreateComment(Comment comment)
    {
        var request = new RequestWrapper();
        return request;
    }

    public async Task<RequestWrapper> LikeComment(int id)
    {
        var request = new RequestWrapper();
        return request;
    }

}