using DataAccessLayer;
using Interfaces;

public class PostService : IPostService
{
    private readonly DataBaseConnection _connection;

    public PostService(DataBaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Post>> GetPosts()
    {
        return new List<Post>();
    }

    public async Task CreatePost(Post post)
    {
    }

    public async Task LikePost(int id)
    {
    }

    public async Task UnlikePost(int id)
    {
    }

}