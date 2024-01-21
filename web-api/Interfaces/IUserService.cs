using DataModels;

namespace Interfaces;

public interface IUserService
{
    Task<RequestWrapper<string>> Login(User user);
    Task<RequestWrapper> SignUp(User user);
    Task<UserModel> GetUserByUsername(string username);
}

