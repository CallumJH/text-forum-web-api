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

    /// <summary>
    /// Log in a user
    /// </summary>
    /// <param name="user">The user values for loging in.</param>
    /// <returns>
    /// 200 OK if login is successful.
    /// 400 Bad Request if the login request is invalid.
    /// 500 Internal Server Error if there is an error.
    /// </returns>
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUser([FromBody] User user)
    {
        try
        {
            //The request is the token in this regard
            var request = await userService.Login(user);
            if (request.Success)
            {
                return Ok(request);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Sign up a user
    /// </summary>
    /// <param name="user">The user values for signing in: Username and password</param>
    /// <returns></returns>
    [HttpPost("Signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUpUser([FromBody] User user)
    {
        //TODO: Include specific error messages when per se username already exists
        try
        {
            var request = await userService.SignUp(user);
            if (request.Success)
            {
                return Ok();
            }
            else
            {
                if(request.Message == "Username already exists")
                {
                    return BadRequest("Username already exists");
                }
                return BadRequest();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}
