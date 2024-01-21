using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "User"), Route("api/[controller]")]
public class CommentsController : Controller
{
    ICommentService commentService;
    private readonly ContextService contextService;
    
    public CommentsController(ICommentService commentService, ContextService contextService)
    {
        this.commentService = commentService;
        this.contextService = contextService;
    }

    /// <summary>
    /// Get comments for a specific post.
    /// </summary>
    /// <param name="postId">The ID of the post </param>
    /// <returns>
    /// </returns>
    [HttpGet("GetComments/{postId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    /// 200 OK if the comment was created successfully.
    /// 400 Bad Request if the comment was not created successfully.
    /// 401 Unauthorized if the token is invalid.
    /// 500 Internal Server Error if there is an error.
    /// </returns>
    [HttpPost("CreateComment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateComment([FromBody] Comment comment)
    {
        try
        {
            var userModel = await contextService.RetrieveUserFromHTTPContext();
            if (userModel == null)
            {
                return StatusCode(400);
            }

            var request = await commentService.CreateComment(comment, userModel);
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
    /// 200 OK if the comment was liked successfully.
    /// 400 Bad Request if the comment was not liked successfully.
    /// 401 Unauthorized if the token is invalid.
    /// 500 Internal Server Error if there is an error.
    /// </returns>
    [HttpPost("LikeComment/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LikeComment(int id)
    {
        try
        {
            var userModel = await contextService.RetrieveUserFromHTTPContext();
            if (userModel == null)
            {
                return StatusCode(400);
            }

            var request = await commentService.LikeComment(id, userModel);
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