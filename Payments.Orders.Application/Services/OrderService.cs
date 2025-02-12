﻿namespace Payments.Orders.Application.Services;

public class OrderService(OrdersDbContext context, ICartService cartsService) : IOrderService
{
    public async Task<OrderDTO> Create(CreateOrderDTO order)
    {

        var orderByOrderNumber = await context.Orders.FirstOrDefaultAsync(x =>
            x.OrderNumber == order.OrderNumber && x.MerchantId == order.MerchantId);

        if (orderByOrderNumber != null)
        {
            throw new DublicateEntityException($"Order with orderNumber {order.OrderNumber} is exist for merchant " +
                                               $"{order.MerchantId}");
        }

        if (order.Cart == null)
        {
            throw new ArgumentNullException();
        }

        var cart = await cartsService.Create(order.Cart);
        var entity = new OrderEntity
        {
            OrderNumber = order.OrderNumber,
            Name = order.Name,
            CustomerId = order.CustomerId,
            CartId = cart.Id
        };

        var orderSaveResult = await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();

        return orderSaveResult.Entity.ToDTO();
    }

    public async Task<OrderDTO> GetById(long orderId)
    {
        var entity = await context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (entity == null) throw new EntityNotFoundException($"Order entity with id {orderId} not found");
        

        return entity.ToDTO();
    }

    public async Task<List<OrderDTO>> GetByUser(long customerId)
    {
        var entity = await context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

        return entity.Select(x => x.ToDTO()).ToList();
    }

    public async Task<List<OrderDTO>> GetAll()
    {
        var entity = await context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .AsNoTracking()
            .ToListAsync();

        return entity.Select(x => x.ToDTO()).ToList();
    }

    //todo: прикрутить статусную модель для отмены заказа
    public async Task<long> Reject(long orderId)
    {
        var order = context.Orders.FirstOrDefault(order => order.Id == orderId);

        if(order == null) throw new EntityNotFoundException($"sad");

        order.Status = OrderStatusType.Rejected;

        await context.SaveChangesAsync();

        return orderId;

    }
}
