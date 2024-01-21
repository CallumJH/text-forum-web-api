using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataModels;
using Interfaces;
using Microsoft.IdentityModel.Tokens;


public class IdentityService : IIdentityService
{
    private readonly string? TokenKey = "b09c31c79593e2b305bb4d60c48dd05908f6c6ab94e96675d0c494270cc2cddf";// Environment.GetEnvironmentVariable("TokenKey");
    private readonly TimeSpan TokenExpiration = TimeSpan.FromHours(1); // Token lifetime is 1 hour currently

    /// <summary>
    /// This is mocking a larger token generation process.
    /// Do not replicate in production.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GenerateToken(UserModel user)
    {
        try
        {
            if (TokenKey == null)
            {
                throw new Exception("TokenKey is null");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.Add(TokenExpiration),
                Issuer = "http://localhost:5000",
                Audience = "",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            if(user.IsModerator == 1)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "Moderator"));
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}