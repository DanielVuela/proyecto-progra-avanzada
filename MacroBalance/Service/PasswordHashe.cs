using System.Security.Cryptography;
using System.Text;

namespace MacroBalance.Service
{
    public static class PasswordHasher
    {
        public static string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (var b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
