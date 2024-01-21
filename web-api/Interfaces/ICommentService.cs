namespace Interfaces;

public interface ICommentService
{
    Task<RequestWrapper> CreateComment(Comment comment);
    Task<RequestWrapper<List<Comment>>> GetComments(int postId);
    Task<RequestWrapper> LikeComment(int id);
}

