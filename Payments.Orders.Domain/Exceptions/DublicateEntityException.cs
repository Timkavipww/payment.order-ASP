namespace Payments.Orders.Domain.Exceptions;

public class DublicateEntityException(string? message = null) : Exception(message)
{
}
