using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;


public class UserService : IUserService
{
    private readonly DataBaseConnection db;
    public UserService(DataBaseConnection dataBaseConnection)
    {
        db = dataBaseConnection;
    }

    public Task<RequestWrapper> Login(User user)
    {
        try
        {
            var response = new RequestWrapper();
            var User = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password); // TODO: Resolve Hash for this comparison
            if (User == null)
            {
                // Keep in mind this is a backend related message don't expose it to the user
                response.Message = "User not found";
                response.Success = false;
                return Task.FromResult(response);
            }
            response.Success = true;
            return Task.FromResult(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var response = new RequestWrapper();
            response.Message = "Something went wrong";
            response.Success = false;
            return Task.FromResult(response);
        }
    }

    public Task<RequestWrapper> SignUp(User user)
    {
        try
        {
            var response = new RequestWrapper();
            var User = new UserModel()
            {
                Username = user.Username,
                Password = user.Password, // TODO: Hash this
                IsModerator = 0
            };
            db.Insert(User);
            response.Success = true;
            return Task.FromResult(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var response = new RequestWrapper();
            response.Message = "Something went wrong";
            response.Success = false;
            return Task.FromResult(response);
        }
    }

}