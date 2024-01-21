using System.Security.Cryptography;
using System.Text;

/// <summary>
/// This class is an extension class used to encrypt and decrypt user passwords.
/// Standardized to use SHA512.
/// See https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha512?view=net-5.0
/// </summary>
public static class EncryptionService
{
    /// <summary>
    /// Encrypts a password using a salt.
    /// </summary>
    /// <param name="password">The users chosen password</param>
    /// <param name="salt">A generated salt to be stored to a user</param>
    /// <returns>An encrypted password</returns>
    public static string EncryptPassword(string password, string salt)
    {
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hmac = new HMACSHA512(saltBytes);
        var hash = hmac.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Generates a salt to be used in password encryption.
    /// </summary>
    /// <returns>A salt to stored alongside a user</returns>
    public static string GenerateSalt()
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
    }

    /// <summary>
    /// Verifies a password against a stored hash and salt.
    /// </summary>
    /// <param name="password">A users attempted password</param>
    /// <param name="salt">The users salt</param>
    /// <param name="hash">The users password hash</param>
    /// <returns>True if password is correct else false</returns>
    public static bool VerifyPassword(string password, string salt, string hash)
    {
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hmac = new HMACSHA512(saltBytes);
        var newHash = hmac.ComputeHash(passwordBytes);
        return Convert.ToBase64String(newHash) == hash;
    }
}