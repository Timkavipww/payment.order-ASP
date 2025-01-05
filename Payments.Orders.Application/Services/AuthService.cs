using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payments.Orders.Application.Abstractions;
using Payments.Orders.Application.Models.Authentication;
using Payments.Orders.Domain.Exceptions;
using Payments.Orders.Domain.Models;
using Payments.Orders.Domain.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;

namespace Payments.Orders.Application.Services;

public class AuthService(
    IOptions<AuthOptions> authOpt,
    UserManager<UserEntity> userManager)
    : IAuthService
{
    private readonly AuthOptions _authOptions = authOpt.Value;
    public async Task<UserResponse> Login(UserLoginDto userLoginModel)
    {
        var user = await userManager.FindByEmailAsync(userLoginModel.Email);

        if(user == null) 
            throw new EntityNotFoundException($"User with email {userLoginModel.Email} not found");

        var checkPasswordResult = await userManager.CheckPasswordAsync(user, userLoginModel.Password);

        if (!checkPasswordResult)
            throw new AuthenticationException("Invalid password");

        var response = await CreateUserResponseAsync(user);
        return GenerateToken(response);

    }

    public async Task<UserResponse> Register(UserRegisterDto userRegisterModel)
    {
        if(await userManager.FindByEmailAsync(userRegisterModel.Email) != null)
            throw new DublicateEntityException($"User with email {userRegisterModel.Email} already exists");

        var createUserResult = await userManager.CreateAsync(new UserEntity
        {
            Email = userRegisterModel.Email,
            UserName = userRegisterModel.Username,
            PhoneNumber = userRegisterModel.Phone
        }, userRegisterModel.Password);

        if(!createUserResult.Succeeded)
            throw new EntityCreationException("User creation failed");

        var user = await userManager.FindByEmailAsync(userRegisterModel.Email);

        if(user == null)
            throw new EntityNotFoundException($"User with email {userRegisterModel.Email} not found");

        var result = await userManager.AddToRoleAsync(user, RoleConsts.User);

        if (!result.Succeeded)
            throw new EntityCreationException("User creation failed");

        var response = await CreateUserResponseAsync(user);

        return GenerateToken(response);

    }
    private async Task<UserResponse> CreateUserResponseAsync(UserEntity user)
    {
        var userRoles = await userManager.GetRolesAsync(user);
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Roles = userRoles.ToArray(),
            Username = user.UserName,
            Phone = user.PhoneNumber
        };
    }

    private UserResponse GenerateToken(UserResponse userResponse)
    {
        var handler = new JwtSecurityTokenHandler();

        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            expires: DateTime.Now.AddSeconds(_authOptions.ExpirationMinutes),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.TokenPrivateKey)),
                SecurityAlgorithms.HmacSha256)
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        userResponse.Token = stringToken;
        return userResponse;
    }
}
