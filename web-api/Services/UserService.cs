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
            var dbUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
            if (dbUser == null)
            {
                // Keep in mind this is a backend related message don't expose it to the user
                response.Message = "User not found";
                return response;
            }
            var salt = dbUser.Salt;
            var providedPassword = EncryptionService.EncryptPassword(user.Password, salt);
            if(providedPassword == dbUser.Password)
            {
                response.SetSucceeded();
                return response;
            }
            response.Message = "Wrong password";
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
            var userSalt = EncryptionService.GenerateSalt();
            var User = new UserModel()
            {
                Username = user.Username,
                Salt = userSalt,
                Password = EncryptionService.EncryptPassword(user.Password, userSalt),
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