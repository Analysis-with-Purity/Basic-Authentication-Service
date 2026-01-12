namespace AuthMicroservice.Services;

public interface IPasswordHashAssist
{
    string HasherPassword(string password);
    string ConvertToHexString(byte[] bytes);
}