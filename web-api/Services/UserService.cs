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

    public async Task<RequestWrapper> Login(User user)
    {
        try
        {
            var response = new RequestWrapper();
            var User = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password); // TODO: Resolve Hash for this comparison
            if (User == null)
            {
                // Keep in mind this is a backend related message don't expose it to the user
                response.Message = "User not found";
                return response;
            }
            response.SetSucceeded();
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var response = new RequestWrapper();
            return response;
        }
    }

    public async Task<RequestWrapper> SignUp(User user)
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
            await db.InsertAsync(User);
            response.SetSucceeded();
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var response = new RequestWrapper();
            return response;
        }
    }

}