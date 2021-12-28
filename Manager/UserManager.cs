using System.Security.Cryptography;
using webApp.Data;
using webApp.Models;

namespace webApp.Manager
{
    public class UserManager
    {
        public static string makeHashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool isValidPassword(User user, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(user.password);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        // public static async Task<Token> loginAsync(User user, ShopContext _context)
        // {
        //     Token savedToken = _context.Tokens.Where<Token>(token => token.User == user).FirstOrDefault();
        //     if (savedToken != null)
        //     {
        //         _context.Remove<Token>(savedToken);
        //         await _context.SaveChangesAsync();
        //     }
        //     string randomToken = "";
        //     do
        //     {
        //         byte[] salt = new byte[32];
        //         new RNGCryptoServiceProvider().GetBytes(salt);
        //         randomToken = Convert.ToBase64String(salt);
        //     } while (_context.Tokens.Where<Token>(t => t.token == randomToken).Any());
        //     Token token = new Token() { token = randomToken, User = user, gen_dateTime = DateTime.Now, exp_dateTime = DateTime.Now.AddDays(7) };
        //     _context.Add(token);
        //     await _context.SaveChangesAsync();
        //     return token;
        // }
    }
}
