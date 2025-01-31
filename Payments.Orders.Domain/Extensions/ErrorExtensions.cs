namespace Payments.Orders.Domain.Extensions;

public static class ErrorExtensions
{
    public static string ToText(this Exception exception) {
        var inner = exception.InnerException;

        return $"exception {exception.Message} {exception.StackTrace} | {inner?.Message} {inner?.StackTrace}";

    }
}
