using DataModels;

namespace Interfaces;

public interface IPostService
{

    Task<RequestWrapper> CreatePost(Post post, UserModel userModel);
    Task<RequestWrapper<List<Post>>> GetPosts(int currentPage);
    Task<RequestWrapper<Post>> GetPost(int id);
    Task<RequestWrapper> LikePost(int id, UserModel userModel);
    Task<RequestWrapper> UnlikePost(int id, UserModel userModel);
}
