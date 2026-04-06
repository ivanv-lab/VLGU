using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Repository.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisingAgency.Service;

public class AuthService
{
    private readonly UserRepository repository;
    private readonly IConfiguration configuration;

    public AuthService(UserRepository repository,
        IConfiguration configuration)
    {
        this.repository = repository;
        this.configuration = configuration;
    }
    
    public async Task<User> findUserByEmail(string email)
    {
        return await
            repository.getByEmail(email);
    }

    public async Task<bool> checkPassword(User user, string password)
    {
        password = hashPassword(password);
        return user.passwordHash
            .Equals(password);
    }

    public string hashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator
            .GetBytes(128 / 8);
        string hashed = Convert.ToBase64String(KeyDerivation
            .Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8
            ));

        return hashed;
    }

    public string generateJwtToken(User user, Role role)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,
                Convert.ToString(user.id)),
            new Claim(JwtRegisteredClaimNames.Name,
                user.fullname),
            new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role.name)
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