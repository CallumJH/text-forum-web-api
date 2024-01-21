namespace Interfaces;

public interface IPostService
{

    Task CreatePost(Post post);
    Task<List<Post>> GetPosts();
    Task LikePost(int id);
    Task UnlikePost(int id);
}
