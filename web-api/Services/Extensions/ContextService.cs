using System.Security.Claims;
using DataModels;
using Interfaces;

public class ContextService
{
    private IUserService userService;
    private IHttpContextAccessor httpContextAccessor;
    public ContextService(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        this.userService = userService;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<UserModel> RetrieveUserFromHTTPContext()
    {
        if(httpContextAccessor.HttpContext == null)
        {
            return new UserModel();
        }
        var user = httpContextAccessor.HttpContext.User;
        var username = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if(username == null)
        {
            return new UserModel();
        }
        var userModel = await userService.GetUserByUsername(username);
        if(userModel == null)
        {
            return new UserModel();
        }
        return userModel;
    }
}