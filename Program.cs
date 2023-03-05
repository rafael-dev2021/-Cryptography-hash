using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int length = 20;
            Console.WriteLine("Gerando uma senha aleatória e segura:");
            string password = GeneratePassword(length);
            Console.WriteLine(password);

            Console.WriteLine("Criptografando a senha...");
            string hashPassword = HashPassword(password);
            Console.WriteLine(hashPassword);
        }

        static string GeneratePassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+={}[]\\|:;<>,.?/";
            StringBuilder sb = new StringBuilder(length);

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    sb.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            return sb.ToString();
        }

        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}