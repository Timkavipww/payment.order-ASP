using Payments.Orders.Application.Models.Orders;

namespace Payments.Orders.Application.Mappers;

public static class OrderMappers
{
    public static OrderDTO ToDTO(this OrderEntity entity, CartEntity? cart = null)
    {
        return new OrderDTO
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            Name = entity.Name,
            OrderNumber = entity.OrderNumber,
            Cart = cart == null ? entity.Cart?.ToDTO() : cart.ToDTO()
        };
    }
    public static OrderEntity ToEntity(this CreateOrderDTO dTO, CartDTO? cart = null)
    {
        return new OrderEntity
        {
            CustomerId = dTO.CustomerId,
            Name = dTO.Name,
            OrderNumber = dTO.OrderNumber,
            Cart = cart?.ToEntity()
        };
    }
}
