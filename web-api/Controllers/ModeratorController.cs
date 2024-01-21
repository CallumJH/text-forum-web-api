using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Moderator"), Route("api/[controller]")]
public class ModeratorController : Controller
{
    IModeratorService moderatorService;
    public ModeratorController(IModeratorService moderatorService)
    {
        this.moderatorService = moderatorService;
    }

    /// <summary>
    /// This controller function is used by moderators to
    /// flag posts and comments inappropriate or misleading.
    /// </summary>
    /// <param name="toggle">This object contains the post/comment id and the flag values</param>
    /// <returns>Http response code of toggle process</returns>
    [HttpPost("ToggleContentFlag")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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