using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Moderator"), Route("api/[controller]")]
public class ModeratorController : Controller
{

    public ModeratorController()
    {
    }

    [HttpPost("togglePostMisleading/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TogglePostMisleading(int id)
    {
        //Default 200 OK
        return Ok();
    }

    [HttpPost("togglePostFalseInformation/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TogglePostFalseInformation(int id)
    {
        //Default 200 OK
        return Ok();
    }
}