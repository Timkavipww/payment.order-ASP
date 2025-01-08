namespace Payments.Orders.Web.Controllers;

//[Route("api/orders")]
[ApiVersion("2")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrdersController(IOrderService orders, ILogger<OrdersController> logger) : ApiBaseController
{
    [HttpPost]
    [MapToApiVersion("2")]

    public async Task<IActionResult> Create(CreateOrderDTO request)
    {
        logger.LogInformation($"Method api/orders Create started. Request: {JsonSerializer.Serialize(request)}");

        var result = await orders.Create(request);

        logger.LogInformation($"Method api/orders Create finished. Request: {JsonSerializer.Serialize(request)}" +
                              $"Response: {JsonSerializer.Serialize(result)}");

        return Ok(result);
    }

    [HttpGet("{orderId:long}")]
    [MapToApiVersion("2")]

    public async Task<IActionResult> GetById(long orderId)
    {
        logger.LogInformation($"Method api/orders/{orderId} started.");

        var result = await orders.GetById(orderId);

        logger.LogInformation($"Method api/orders/{orderId} finished. " +
                              $"Response: {JsonSerializer.Serialize(result)}");

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    [MapToApiVersion("2")]

    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Method api/orders GetAll started.");

        var result = await orders.GetAll();

        logger.LogInformation($"Method api/orders GetAll finished. Result count: {result.Count}");

        return Ok(new { orders = result });
    }

    [HttpGet("customers/{customerId:long}")]
    [MapToApiVersion("2")]

    public async Task<IActionResult> GetByUser(long customerId)
    {
        logger.LogInformation($"Method api/orders/customers/{customerId} GetByUser started.");

        var result = await orders.GetByUser(customerId);

        logger.LogInformation($"Method api/orders/customers/{customerId} GetByUser finished. Result count: {result.Count}");

        return Ok(new { orders = result });
    }

    [HttpPost("{orderId:long}/reject")]
    [MapToApiVersion("2")]

    public async Task<IActionResult> Reject(long orderId)
    {
        await orders.Reject(orderId);

        return Ok();
    }
}