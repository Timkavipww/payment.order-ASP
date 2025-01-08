namespace Payments.Orders.Web.Controllers;


[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CustomerController(ICustomerService _customerService, ILogger<CustomerController> _logger) : ApiBaseController
{
    [MapToApiVersion("1")]
    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> Create(CreateCustomerDTO request)
    {
        _logger.LogInformation($"Method api/customers Create started. Request: {JsonSerializer.Serialize(request)}");

        var result = await _customerService.Create(request);

        _logger.LogInformation($"Method api/customers Create finished. Request: {JsonSerializer.Serialize(request)}" +
                              $"Response: {JsonSerializer.Serialize(result)}");

        return Ok(result);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1")]

    public async Task<ActionResult<CustomerDTO>> GetById(long id)
    {
        var customer = await _customerService.GetById(id);

        return Ok(customer);
    }

    [HttpGet]
    [MapToApiVersion("1")]

    public async Task<ActionResult<List<CustomerDTO>>> GetAll()
    {
        var customers = await _customerService.GetAll();

        return Ok(customers);
    }
    [HttpGet]
    [MapToApiVersion("2")]
    public async Task<ActionResult<List<CustomerDTO>>> GetAllV2()
    {
        var customers = await _customerService.GetAll();

        return Ok(customers);
    }
}
