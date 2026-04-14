using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AdvertisingAgency.Repository.Authorization;
using AdvertisingApplication.Model.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisingApplication.Service;

public class AuthService
{
    private readonly UserRepository repository;
    private readonly IConfiguration configuration;
    private readonly IPasswordHasher<User> passwordHasher;

    public AuthService(UserRepository repository,
        IConfiguration configuration,
        IPasswordHasher<User> passwordHasher)
    {
        this.repository = repository;
        this.configuration = configuration;
        this.passwordHasher = passwordHasher;
    }

    public async Task<User> findUserByEmail(string email)
    {
        return await
            repository.getByEmail(email);
    }

    public async Task<bool> isUserExists(string email)
    {
        return await
            repository.isUserExists(email);
    }

    public async Task<bool> checkPassword(User user, string password)
    {
        var result = passwordHasher
            .VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Success)
            return true;
        else return false;
    }

    public string hashPassword(User user,string password)
    {
        return passwordHasher
            .HashPassword(user, password);
    }

    public string generateJwtToken(User user, Role role)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,
                Convert.ToString(user.Id)),
            new Claim(JwtRegisteredClaimNames.Name,
                user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble
                (configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}