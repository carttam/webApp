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
            RandomNumberGenerator.Create().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        private static bool isValidPassword(User user, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(user.password!);
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

        public static async Task<Token?> loginAsync(string username, string password, IUnitOfWork _unitOfWork)
        {
            User? user =
                await _unitOfWork.User.FirstOrDefaultAsync(u => u.username == username);
            if (user != null && isValidPassword(user,password))
            {
                Token? savedToken = await _unitOfWork.Token!.FirstOrDefaultAsync(token => token.User == user);
                if (savedToken != null)
                {
                    _unitOfWork.Token.Remove(savedToken);
                    await _unitOfWork.SaveAsync();
                }

                string randomToken = "";
                do
                {
                    byte[] salt = new byte[32];
                    RandomNumberGenerator.Create().GetBytes(salt);
                    randomToken = Convert.ToBase64String(salt);
                } while (await _unitOfWork.Token!.FirstOrDefaultAsync(t => t.token == randomToken) != null);

                Token token = new Token()
                {
                    token = randomToken, User = user, gen_dateTime = DateTime.Now,
                    exp_dateTime = DateTime.Now.AddDays(7)
                };
                await _unitOfWork.Token.AddAsync(token);
                await _unitOfWork.SaveAsync();
                return token;
            }

            return null;
        }
    }
}