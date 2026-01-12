using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using AuthMicroservice.Services;

namespace Infrastructure.TechnicalImplementations;

public class PasswordHasherAuth: IPasswordHashAssist
{

    public string HasherPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            string hashedString = ConvertToHexString(hashBytes);

            return hashedString;
        }
    }

   
    public string ConvertToHexString(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}