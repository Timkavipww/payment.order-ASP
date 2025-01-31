namespace Payments.Orders.Web.Models;

public class ApiErrorResponse
{
    public required string Message { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; } 
    public object? Result { get; set; }

}
