namespace Payments.Orders.Domain.Exceptions;

public class EntityCreationException(string? message = null) : Exception(message)
{
}
