using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Moderator")]
public class ModeratorController : Controller
{
    private readonly DataBaseConnection _connection;

    public ModeratorController(DataBaseConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("togglePostMisleading/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> TogglePostMisleading(int id)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpPost("togglePostFalseInformation/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> TogglePostFalseInformation(int id)
    {
        //Default 200 OK
        return Task.FromResult<IActionResult>(Ok());
    }
}