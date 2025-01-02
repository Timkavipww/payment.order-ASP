using Microsoft.EntityFrameworkCore;
using Payments.Orders.Application.Abstractions;
using Payments.Orders.Application.Mappers;
using Payments.Orders.Application.Models.Customers;
using Payments.Orders.Domain;
using System.Runtime.InteropServices;

namespace Payments.Orders.Application.Services;

public class CustomerService(OrdersDbContext _context) : ICustomerService
{
    public async Task<CustomerDTO> Create(CreateCustomerDTO customer)
    {
        if (customer == null) throw new ArgumentNullException();

        var entity = customer.ToEntity();

        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.ToDTO();


    }
    public async Task<CustomerDTO> GetById(long id)
    {
        if (id < 0) throw new ArgumentNullException();

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if(customer == null) throw new ArgumentNullException();

        return customer.ToDTO();
    }

    public async Task<List<CustomerDTO>> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();

        return customers.Select(c => c.ToDTO()).ToList();
    }

}
