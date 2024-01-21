namespace Interfaces;

public interface IUserService
{
    Task<bool> Login(User user);
    Task<bool> SignUp(User user);
}

