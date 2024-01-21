using DataAccessLayer;
using Interfaces;

public class CommentService : ICommentService
{
    private readonly DataBaseConnection _connection;

    public CommentService(DataBaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Comment>> GetComments(int postId)
    {
        return new List<Comment>();
    }

    public async Task CreateComment(Comment comment)
    {
    }

    public async Task LikeComment(int id)
    {
    }

}