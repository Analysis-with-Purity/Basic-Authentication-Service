using AuthMicroservice.Services;

namespace Infrastructure.TechnicalImplementations;

public class IdGenerator:IIdGenerator
{
    public string GenerateUserId()
    {
        // Static Prefix  "SEN"
        var prefix = "SEN";

        // Append timestamp 
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

        return $"{prefix}{timestamp}";
    }
}