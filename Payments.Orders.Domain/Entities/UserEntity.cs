namespace Payments.Orders.Domain.Entities;

public class UserEntity : IdentityUser<long>
{
    public long MerchantId {get;set;}
    public MerchantEntity Merchant {get;set;}
}
