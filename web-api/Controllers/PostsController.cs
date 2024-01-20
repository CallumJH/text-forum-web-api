using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

public class PostsController : Controller
{
    private readonly DataBaseConnection _connection;

    public PostsController(DataBaseConnection connection)
    {
        _connection = connection;
    }

    [HttpGet("posts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetPosts()
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }

} 