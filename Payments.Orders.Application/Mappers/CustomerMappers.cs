using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Payments.Orders.Application.Models.Customers;

namespace Payments.Orders.Application.Mappers;

public static class CustomerMappers
{
    public static CustomerEntity ToEntity(this CreateCustomerDTO dto)
    {
        return new CustomerEntity
        {
            Email = dto.Email,
            FirstName = dto.Name.Split(' ')[0],
            MiddleName = dto.Name.Split(' ')[1],
            LastName = dto.Name.Split(' ')[2],
            Phone = dto.Phone,
        };
    }

    public static CustomerDTO ToDTO(this CustomerEntity entity)
    {
        return new CustomerDTO
        {
            Email = entity.Email,
            Id = entity.Id,
            Name = string.Join(' ', entity.FirstName, entity.MiddleName, entity.LastName),
            Phone = entity.Phone,
        };
    }
}
