using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using final_proj.DTO;
using final_proj.Models;
using final_proj.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace final_proj.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterUserDto dto);

    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
}

public class AuthService : IAuthService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    private readonly Db26TeamoneContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(Db26TeamoneContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task RegisterAsync(RegisterUserDto dto)
    {
        var normalizedUsername = dto.Username.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedUsername))
        {
            throw new BadRequestApiException("Username is required.");
        }

        var usernameExists = await _context.AppUsers.AnyAsync(u => u.Username == normalizedUsername);
        if (usernameExists)
        {
            throw new ConflictApiException("Username already exists.");
        }

        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(dto.Password),
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        var user = new AppUser
        {
            Username = normalizedUsername,
            PasswordSalt = Convert.ToBase64String(saltBytes),
            PasswordHash = Convert.ToBase64String(hashBytes),
            CreatedAt = DateTime.UtcNow
        };

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var normalizedUsername = dto.Username.Trim().ToLowerInvariant();
        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Username == normalizedUsername);
        if (user == null)
        {
            throw new UnauthorizedApiException("Invalid username or password.");
        }

        var saltBytes = Convert.FromBase64String(user.PasswordSalt);
        var expectedHash = Convert.FromBase64String(user.PasswordHash);
        var incomingHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(dto.Password),
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        if (!CryptographicOperations.FixedTimeEquals(expectedHash, incomingHash))
        {
            throw new UnauthorizedApiException("Invalid username or password.");
        }

        var token = GenerateJwtToken(user.Username);
        return token;
    }

    private LoginResponseDto GenerateJwtToken(string username)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "THIS_IS_A_DEV_ONLY_CHANGE_ME_1234567890";
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? "final_proj";
        var jwtAudience = _configuration["Jwt:Audience"] ?? "final_proj_clients";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(4);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.NameIdentifier, username)
        };

        var tokenDescriptor = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return new LoginResponseDto
        {
            Token = token,
            ExpiresAtUtc = expires
        };
    }
}
