using System.Security.Cryptography;
using System.Text;

namespace AdvertisementApp.Common.Helpers;

public class Helper
{
    public static string Hash(string word)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(word);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in hashBytes)
            {
                stringBuilder.Append(item.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
