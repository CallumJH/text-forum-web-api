using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[Authorize(Roles = "User"), Route("api/[controller]")]
[Route("api/[controller]")]
public class PostsController : Controller
{
    IPostService postService;
    public PostsController(IPostService postService)
    {
        this.postService = postService;
    }

    /// <summary>
    /// Get all posts.
    /// NB. THIS WILL REQUIRE PAGINATION IN THE FUTURE.
    /// </summary>
    /// <returns>
    /// 200 OK and all posts.
    /// 400 Bad Request if there are no posts.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("posts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPosts()
    {
        try
        {
            var posts = await postService.GetPosts();
            if(!posts.Success)
            {
                return NotFound();
            }
            return Ok(posts);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    /// <summary>
    /// Get a specific post.
    /// </summary>
    /// <param name="id">The ID of the post </param>
    /// <returns>
    /// 200 OK and the post if the post exists.
    /// 400 Bad Request if the requested post id is invalid.
    /// 404 Not Found if the post does not exist.
    /// 500 Internal Server Error if there is an error.
    /// </returns>
    [HttpGet("posts/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPost(int id)
    {
        try
        {
            var post = await postService.GetPost(id);
            if(!post.Success)
            {
                return NotFound();
            }
            return Ok(post);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    /// <summary>
    /// Create a post.
    /// </summary>
    /// <param name="post">
    /// Post object containing required post values
    /// </param>
    /// <returns>
    /// 200 OK if the post is created.
    /// 400 Bad Request if the post request is invalid.
    /// 500 Internal Server Error if there is an error.
    /// 511 Network Authentication Required if the user is not logged in.
    /// </returns>
    [HttpPost("createPost")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status511NetworkAuthenticationRequired)]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        try
        {
            var request = await postService.CreatePost(post);
            if(!request.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    /// <summary>
    /// Like a post via given post id.
    /// </summary>
    /// <param name="id">
    /// The ID of the post to like.
    /// </param>
    /// <returns>
    /// 200 OK if the post is liked.
    /// 400 Bad Request if the post request is invalid.
    /// 500 Internal Server Error if there is an error.
    /// 511 Network Authentication Required if the user is not logged in.
    /// </returns>
    [HttpPost("likePost/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status511NetworkAuthenticationRequired)]
    public async Task<IActionResult> LikePost(int id)
    {
        try
        {
            var request = await postService.LikePost(id);
            if(!request.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    /// <summary>
    /// Like a post via given post id.
    /// </summary>
    /// <param name="id">
    /// The ID of the post to like.
    /// </param>
    /// <returns>
    /// 200 OK if the post is liked.
    /// 400 Bad Request if the post request is invalid.
    /// 500 Internal Server Error if there is an error.
    /// 511 Network Authentication Required if the user is not logged in.
    /// </returns>
    [HttpPost("unlikePost/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status511NetworkAuthenticationRequired)]
    public async Task<IActionResult> UnlikePost(int id)
    {
        try
        {
            var request = await postService.UnlikePost(id);
            if(!request.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
} 