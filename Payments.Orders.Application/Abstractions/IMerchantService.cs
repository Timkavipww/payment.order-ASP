using Payments.Orders.Application.Models.Merchants;

namespace Payments.Orders.Application.Abstractions;

public interface IMerchantService
{
    Task<MerchantDto> Create(MerchantDto merchant);
}
