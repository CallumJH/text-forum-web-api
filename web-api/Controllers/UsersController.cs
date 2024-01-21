using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class UsersController : Controller
{
    IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> LoginUser([FromBody] User user)
    {
        try
        {
            var request = userService.Login(user);
            if (request.Result.Success)
            {
                return Task.FromResult<IActionResult>(Ok());
            }
            else
            {
                return Task.FromResult<IActionResult>(BadRequest());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult<IActionResult>(StatusCode(500));
        }
    }

    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> SignUpUser([FromBody] User user)
    {
        //TODO: Include specific error messages when per se username already exists
        try
        {
            var request = userService.SignUp(user);
            if (request.Result.Success)
            {
                return Task.FromResult<IActionResult>(Ok());
            }
            else
            {
                return Task.FromResult<IActionResult>(BadRequest());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult<IActionResult>(StatusCode(500));
        }
    }
}
