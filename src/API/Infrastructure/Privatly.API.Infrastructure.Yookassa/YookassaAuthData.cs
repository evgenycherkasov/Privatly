namespace Privatly.API.Infrastructure.Yookassa;

public record YookassaAuthData
{
    public string ShopId { get; set; }

    public string SecretKey { get; set; }
}