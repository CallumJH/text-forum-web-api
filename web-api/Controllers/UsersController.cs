using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            if (request.Result)
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
        try
        {
            var request = userService.SignUp(user);
            if (request.Result)
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
