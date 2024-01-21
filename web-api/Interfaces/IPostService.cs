namespace Interfaces;

public interface IPostService
{

    Task<RequestWrapper> CreatePost(Post post);
    Task<RequestWrapper<List<Post>>> GetPosts();
    Task<RequestWrapper<Post>> GetPost(int id);
    Task<RequestWrapper> LikePost(int id);
    Task<RequestWrapper> UnlikePost(int id);
}
