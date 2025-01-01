using Microsoft.EntityFrameworkCore;
using Payments.Orders.Application.Abstractions;
using Payments.Orders.Application.Mappers;
using Payments.Orders.Domain;

namespace Payments.Orders.Application.Services;

public class CartService(OrdersDbContext _context) : ICartService
{
    public async Task<CartDTO> Create(CartDTO cartDTO)
    {
        var cartEntity = new CartEntity();
        var cartSaveResult = await _context.Carts.AddAsync(cartEntity);

        await _context.SaveChangesAsync();

        var cartItems = cartDTO.CartItems
            .Select(item => new CartItemEntity
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity
            });

        await _context.CartItems.AddRangeAsync(cartItems);
        await _context.SaveChangesAsync();

        var result = await _context.Carts
            .Include(x => x.CartItems)
            .FirstAsync(x => x.Id == cartSaveResult.Entity.Id);

        return result.ToDTO();


    }
}
