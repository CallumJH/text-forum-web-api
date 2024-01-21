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

    public Task<bool> Login(User user)
    {
        try
        {
            var User = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password); // TODO: Resolve Hash for this comparison
            if (User == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
        }
    }

    public Task<bool> SignUp(User user)
    {
        try
        {
            var User = new UserModel()
            {
                Username = user.Username,
                Password = user.Password, // TODO: Hash this
                IsModerator = 0
            };
            db.Insert(User);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
        }
    }

}