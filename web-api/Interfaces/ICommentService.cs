namespace Interfaces;

public interface ICommentService
{
    Task CreateComment(Comment comment);
    Task<List<Comment>> GetComments(int postId);
    Task LikeComment(int id);
}

