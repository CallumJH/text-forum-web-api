using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "User"), Route("api/[controller]")]
public class CommentsController : Controller
{

    public CommentsController()
    {
    }

    /// <summary>
    /// Get comments for a specific post.
    /// </summary>
    /// <param name="postId">The ID of the post </param>
    /// <returns>
    /// </returns>
    [HttpGet("comments/{postId}")]
    public Task<IActionResult> GetComments(int postId)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok(new List<Comment>()));
    }

    /// <summary>
    /// Create a comment.
    /// </summary>
    /// <param name="post">
    /// Post object containing required comment values
    /// </param>
    /// <returns>
    /// </returns>
    [HttpPost("createComment")]
    public Task<IActionResult> CreateComment([FromBody] Comment comment)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>
    /// Like a comment via given comment id.
    /// </summary>
    /// <param name="id">
    /// The ID of the comment to like.
    /// </param>
    /// <returns>
    /// </returns>
    [HttpPost("likeComment/{id}")]
    public Task<IActionResult> LikeComment(int id)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }
} 