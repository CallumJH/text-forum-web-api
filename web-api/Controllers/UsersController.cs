using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

public class UsersController : Controller
{
    public UsersController()
    {
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> LoginUser([FromBody] User user)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> SignUpUser([FromBody] User user)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }
}
