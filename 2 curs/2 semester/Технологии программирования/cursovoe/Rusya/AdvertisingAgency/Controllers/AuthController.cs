using AdvertisingAgency.Contract.User;
using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingAgency.Controllers;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController:ControllerBase
{
    private readonly AuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> login([FromBody] AuthContract
        contract)
    {
        User user = await authService
            .findUserByEmail(contract.email);

        if (user == null || !await authService
                .checkPassword(user, contract.password))
            return Unauthorized("Invalid credentials");

        Role role = user.role;
        string token = authService
            .generateJwtToken(user, role);
        return Ok(new { token });
    }
}