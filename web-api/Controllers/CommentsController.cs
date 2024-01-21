using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "User"), Route("api/[controller]")]
public class CommentsController : Controller
{
    ICommentService commentService;
    public CommentsController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    /// <summary>
    /// Get comments for a specific post.
    /// </summary>
    /// <param name="postId">The ID of the post </param>
    /// <returns>
    /// </returns>
    [HttpGet("comments/{postId}")]
    public async Task<IActionResult> GetComments(int postId)
    {
        try
        {
            var comments = await commentService.GetComments(postId);
            if (!comments.Success)
            {
                return NotFound();
            }
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
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
    public async Task<IActionResult> CreateComment([FromBody] Comment comment)
    {
        try
        {
            var request = await commentService.CreateComment(comment);
            if (!request.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
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
    public async Task<IActionResult> LikeComment(int id)
    {
        try
        {
            var request = await commentService.LikeComment(id);
            if (!request.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
} 