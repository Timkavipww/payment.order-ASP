using Payments.Orders.Application.Models.Carts;

namespace Payments.Orders.Application.Mappers;

public static class CartMappers
{
    public static CartDTO ToDTO(this CartEntity entity)
    {
        return new CartDTO
        {
            Id = entity.Id,
            CartItems = entity.CartItems!.Select(item => new CartItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
            }).ToList()
        };
    }
    public static CartEntity ToEntity(this CartDTO cartDTO)
    {
        return new CartEntity
        {
            CartItems = cartDTO.CartItems.Select(cart => cart.ToEntity()).ToList()
        };
    }

    public static CartItemEntity ToEntity(this CartItemDTO dto)
    {
        return new CartItemEntity
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
        };
    }
}

