using AdvertisingApplication.Contract.User;
using AdvertisingApplication.Model.Authorization;
using AdvertisingApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingApplication.Controllers
{
    public class AuthController:Controller
    {
        private readonly AuthService service;

        public AuthController(AuthService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> LoginForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthContract contract)
        {
            User user = null;
            if (await service.isUserExists(contract.email))
            {
                user = await service
                    .findUserByEmail(contract.email);

                if (user == null || !await service
                        .checkPassword(user, contract.password))
                    return Unauthorized("Invalid credentials");
            }
            else return Unauthorized("Invalid credentials");

            Role role = user.role;
            string token = service
                .generateJwtToken(user, role);

            return Ok(new { token });
        }
    }
}
