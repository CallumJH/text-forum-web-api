using DataAccessLayer;
using Interfaces;

public class PostService : IPostService
{
    private readonly DataBaseConnection _connection;

    public PostService(DataBaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<RequestWrapper<List<Post>>> GetPosts()
    {
        var request = new RequestWrapper<List<Post>>();
        return request;
    }

    public async Task<RequestWrapper<Post>> GetPost(int id)
    {
        var request = new RequestWrapper<Post>();
        return request;
    }

    public async Task<RequestWrapper> CreatePost(Post post)
    {
        var request = new RequestWrapper();
        return request;
    }

    public async Task<RequestWrapper> LikePost(int id)
    {
        var request = new RequestWrapper();
        return request;
    }

    public async Task<RequestWrapper> UnlikePost(int id)
    {
        var request = new RequestWrapper();
        return request;
    }

}