namespace Payments.Orders.Application.Abstractions;

public interface ICartService
{
    Task<CartDTO> Create(CartDTO cartDTO);
}
