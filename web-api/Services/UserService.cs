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
                // Keep in mind this is a backend related message don't expose it to the user
                response.Message = "User not found";
                return response;
            }
            var salt = dbUser.Salt;
            var providedPassword = EncryptionService.EncryptPassword(user.Password, salt);
            if(providedPassword == dbUser.Password)
            {
                var userSessionToken = await RetrieveUserSessionToken(dbUser, dbUser.Id);
                response.SetSucceeded();
                response.Data = userSessionToken;
                return response;
            }
            response.Message = "Wrong password";
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var response = new RequestWrapper<string>();
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

    private async Task<string> RetrieveUserSessionToken(UserModel user, int userId)
    {
        var session = await db.Sessions.FirstOrDefaultAsync(x => x.UserId == userId);
        if(session is null)
        {
            var jwtToken = identityService.GenerateToken(user);
            var sessionModel = new SessionModel()
            {
                UserId = userId,
                Token = jwtToken,
                ExpiresAt = DateTime.Now.AddHours(1)
            };
            await db.InsertAsync(sessionModel);
            return jwtToken;
        }
        if(session.ExpiresAt > DateTime.Now)
        {
            var jwtToken = identityService.GenerateToken(user);
            var sessionModel = new SessionModel()
            {
                UserId = userId,
                Token = jwtToken,
                ExpiresAt = DateTime.Now.AddHours(1)
            };
            await db.UpdateAsync(sessionModel);
        }
        return session.Token;
    }

}