using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;
using LinqToDB.Tools;


public class UserService : IUserService
{
    private readonly DataBaseConnection db;
    private readonly IIdentityService identityService;
    public UserService(DataBaseConnection dataBaseConnection, IIdentityService identityService)
    {
        db = dataBaseConnection;
        this.identityService = identityService;
    }

    public async Task<RequestWrapper<string>> Login(User user)
    {
        try
        {
            var response = new RequestWrapper<string>();
            var dbUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
            if (dbUser == null)
            {
                return response.SetFailed("User not found");
            }

            var salt = dbUser.Salt;
            var providedPassword = EncryptionService.EncryptPassword(user.Password, salt);

            if(providedPassword == dbUser.Password)
            {
                var userSessionToken = await RetrieveUserSessionToken(dbUser, dbUser.Id);
                return response.SetSucceeded(userSessionToken);
            }
            return response.SetFailed("Wrong password");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper<string>();
        }
    }

    public async Task<RequestWrapper> SignUp(User user)
    {
        try
        {
            var response = new RequestWrapper();
            var userSalt = EncryptionService.GenerateSalt();
            var password = EncryptionService.EncryptPassword(user.Password, userSalt);
            var User = new UserModel(user.Username, userSalt,password);
            await db.InsertAsync(User);
            return response.SetSucceeded();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper();
        }
    }

    private async Task<string> RetrieveUserSessionToken(UserModel user, int userId)
    {
        var session = await db.Sessions.FirstOrDefaultAsync(x => x.UserId == userId);
        if(session is null)
        {
            var jwtToken = identityService.GenerateToken(user);
            var sessionModel = new SessionModel(userId, jwtToken);
            await db.InsertAsync(sessionModel);
            return jwtToken;
        }
        if(session.ExpiresAt > DateTime.Now)
        {
            var jwtToken = identityService.GenerateToken(user);
            var sessionModel = new SessionModel(userId, jwtToken);
            await db.UpdateAsync(sessionModel);
        }
        return session.Token;
    }

    public async Task<UserModel> GetUserByUsername(string username)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Username == username);
        if(user is null)
        {
            throw new Exception("User not found");
        }
        return user;
    }

}