namespace Payments.Orders.Web.Controllers;

[Route("api/customers")]
public class CustomerController(ICustomerService _customerService, ILogger<CustomerController> _logger) : ApiBaseController
{
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
    public async Task<ActionResult<CustomerDTO>> GetById(long id)
    {
        var customer = await _customerService.GetById(id);

        return Ok(customer);
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAll()
    {
        var customers = await _customerService.GetAll();

        return Ok(customers);
    }

}
