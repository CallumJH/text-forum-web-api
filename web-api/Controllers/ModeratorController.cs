using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// [Authorize(Roles = "Moderator"), Route("api/[controller]")]
[Route("api/[controller]")]
public class ModeratorController : Controller
{
    IModeratorService moderatorService;
    public ModeratorController(IModeratorService moderatorService)
    {
        this.moderatorService = moderatorService;
    }

    [HttpPost("toggleContentFlag")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ToggleContentFlag([FromBody]ContentToggle toggle)
    {
        try
        {
            var request = await moderatorService.ToggleContentFlag(toggle);
            if (request.Success)
            {
                return Ok();
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
}