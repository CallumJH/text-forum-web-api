using DataModels;
namespace Interfaces;

public interface IIdentityService
{
    string GenerateToken(UserModel user);
}
