using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;

public class PostService : IPostService
{
    private readonly DataBaseConnection connection;

    public PostService(DataBaseConnection connection)
    {
        this.connection = connection;
    }

    public async Task<RequestWrapper<List<Post>>> GetPosts()
    {
        try
        {
            var request = new RequestWrapper<List<Post>>();
            var posts = await connection.GetTable<PostModel>().ToListAsync(); //TODO: Change this to pagination
            request.Success = true;
            request.Data = posts.Select(x => x.MapToPost(x.LikeCount)).ToList();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper<List<Post>>();
            request.Message = "Something went wrong";
            request.Success = false;
            return request;
        }
    }

    public async Task<RequestWrapper<Post>> GetPost(int id)
    {
        try
        {
            var request = new RequestWrapper<Post>();
            var post = await connection.GetTable<Post>().FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                request.Message = "Post not found";
                request.Success = false;
                return request;
            }
            request.Success = true;
            request.Data = post;
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper<Post>();
            request.Message = "Something went wrong";
            request.Success = false;
            return request;
        }
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