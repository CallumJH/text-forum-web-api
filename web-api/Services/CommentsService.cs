using DataAccessLayer;

public class CommentsService
{
    private readonly DataBaseConnection _connection;

    public CommentsService(DataBaseConnection connection)
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