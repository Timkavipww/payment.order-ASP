namespace Payments.Orders.Domain.Entities;

public class MerchantEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public string? Website { get; set; }
    public IEnumerable<UserEntity> Users { get; set; }

}
