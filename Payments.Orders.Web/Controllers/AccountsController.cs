using Asp.Versioning;

namespace Payments.Orders.Web.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountsController(IAuthService _authService) : ApiBaseController
{
    [HttpPost("login")]
    [MapToApiVersion("1")]
    public async Task<ActionResult<UserResponse>> Login(UserLoginDto userLoginDto)
    {
        var result = await _authService.Login(userLoginDto);

        return Ok(result);
    }
    [HttpPost("register")]
    [MapToApiVersion("1")]
    public async Task<ActionResult<UserResponse>> Register(UserRegisterDto userRegisterDto)
    {
        var result = await _authService.Register(userRegisterDto);

        return Ok(result);
    }
}
