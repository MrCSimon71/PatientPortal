using System.Security.Cryptography;
using System.Text;


namespace PDDS.PatientData.Core.Helpers
{
    public class PasswordHelper
    {
        public static string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string plainTextPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, passwordHash);
        }

        public static string GenerateRandomPassword()
        {
            var capitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            var smallLetters = "qwertyuiopasdfghjklzxcvbnm";
            var digits = "0123456789";
            var specialCharacters = "!@#$%^&*()-_=+<,>.";
            var allChar = capitalLetters + smallLetters + digits + specialCharacters;
            var passwordLength = 12;

            StringBuilder sb = new StringBuilder();
            for (int n = 0; n < passwordLength; n++)
            {
                sb = sb.Append(GenerateChar(allChar));
            }

            return sb.ToString();
        }

        private static char GenerateChar(string availableChars)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            var byteArray = new byte[1];
            char c;

            do
            {
                provider.GetBytes(byteArray);
                c = (char)byteArray[0];

            } while (!availableChars.Any(x => x == c));

            return c;
        }
    }
}
