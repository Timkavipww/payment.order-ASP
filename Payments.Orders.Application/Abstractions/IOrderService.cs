using Payments.Orders.Application.Models.Orders;

namespace Payments.Orders.Application.Abstractions;

public interface IOrderService
{
    Task<OrderDTO> Create(CreateOrderDTO orderDTO);
    Task<OrderDTO> GetById(long orderId);
    Task<List<OrderDTO>> GetByUser(long customerId);
    Task<List<OrderDTO>> GetAll();
    Task Reject(long orderId);
}
