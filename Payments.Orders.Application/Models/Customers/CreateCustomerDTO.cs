namespace Payments.Orders.Application.Models.Customers;

public class CreateCustomerDTO
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone {  get; set; } = null!;
}
