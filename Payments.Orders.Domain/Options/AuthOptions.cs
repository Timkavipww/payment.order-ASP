namespace Payments.Orders.Domain.Options;

public class AuthOptions
{
    public required string TokenPrivateKey { get; set; }
    public int ExpirationMinutes { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
