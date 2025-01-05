using Payments.Orders.Application.Abstractions;
using Payments.Orders.Application.Models.Merchants;
using Payments.Orders.Domain;

namespace Payments.Orders.Application.Services;
public class MerchantService(OrdersDbContext _context) : IMerchantService
{
    public async Task<MerchantDto> Create(MerchantDto merchant)
    {
        var entity = new MerchantEntity
        {
            Name = merchant.Name,
            Phone = merchant.Phone,
            Website = merchant.Website
        };

        var result = await _context.Merchants.AddAsync(entity);

        await _context.SaveChangesAsync();

        return new MerchantDto
        {
            Id = result.Entity.Id,
            Name = result.Entity.Name,
            Phone = result.Entity.Phone,
            Website = result.Entity.Website
        };
    }
}
