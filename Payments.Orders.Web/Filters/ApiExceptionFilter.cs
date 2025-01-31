using Microsoft.AspNetCore.Mvc.Filters;
using Payments.Orders.Domain.Exceptions;
using Payments.Orders.Domain.Extensions;
using Payments.Orders.Web.Models;

namespace Payments.Orders.Web.Filters;

public class ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) : IExceptionFilter
{


    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        ApiErrorResponse? response;
        int statusCode = 400; 
        switch (true)
        {
            case { } when exception is DublicateEntityException:
            {
                response = new ApiErrorResponse
                {
                    Code = "0001",
                    Message = exception.Message,
                    Description = exception.ToText()
                };

                break;   
            }

            case { } when exception is EntityNotFoundException:
            {
                statusCode = 404;
                response = new ApiErrorResponse
                {
                    Code = "0002",
                    Message = exception.Message,
                    Description = exception.ToText()
                };

                break;   
            }


            default:
                response = new ApiErrorResponse{
                    Code = "0000",
                    Message = exception.Message,
                    Description = exception.ToText()
                };

                break;
            
        }
        logger.LogError($"API {context.HttpContext.Request.Path} finished with code {statusCode} error: \n");
        logger.LogError($"{JsonSerializer.Serialize(response)} ");

        context.Result = new JsonResult(new {response}){StatusCode = statusCode};
    }

}
