using Payments.Orders.Application.Models.Customers;

namespace Payments.Orders.Application.Abstractions;

public interface ICustomerService
{
    Task<CustomerDTO> Create(CreateCustomerDTO customer);
    Task<CustomerDTO> GetById(long id);
    Task<List<CustomerDTO>> GetAll();

}
