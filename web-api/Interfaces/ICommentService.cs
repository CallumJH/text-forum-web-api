using DataModels;

namespace Interfaces;

public interface ICommentService
{
    Task<RequestWrapper> CreateComment(Comment comment, UserModel userModel);
    Task<RequestWrapper<List<Comment>>> GetComments(int postId);
    Task<RequestWrapper> LikeComment(int id, UserModel userModel);
}

