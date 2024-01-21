namespace Interfaces;

public interface IUserService
{
    Task<RequestWrapper> Login(User user);
    Task<RequestWrapper> SignUp(User user);
}

