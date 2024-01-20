using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

public class UsersController : Controller
{
    private readonly DataBaseConnection _connection;

    public UsersController(DataBaseConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> SignUpUser([FromBody] User user)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }
}
