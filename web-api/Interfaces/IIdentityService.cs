namespace Interfaces;

public interface IIdentityService
{
    string GenerateToken(User user);
}
