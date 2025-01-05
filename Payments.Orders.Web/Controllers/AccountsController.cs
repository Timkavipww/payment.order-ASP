using Payments.Orders.Application.Models.Authentication;

namespace Payments.Orders.Web.Controllers;

[Route("accounts")]
public class AccountsController(IAuthService _authService) : ApiBaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(UserLoginDto userLoginDto)
    {
        var result = await _authService.Login(userLoginDto);

        return Ok(result);
    }
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(UserRegisterDto userRegisterDto)
    {
        var result = await _authService.Register(userRegisterDto);

        return Ok(result);
    }
}
