using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;

    public AuthController(IRefreshTokenStore refreshTokenStore, IConfiguration configuration)
    {
        _refreshTokenStore = refreshTokenStore;
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel request)
    {
        if ((request.Username == "test" && request.Password == "password") ||
            (request.Username == "admin" && request.Password == "secured"))
        {
            var accessToken = GenerateJwtToken(request.Username);
            var refreshToken = GenerateRefreshToken();

            _refreshTokenStore.StoreToken(refreshToken, request.Username);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] TokenRefreshRequest request)
    {
        if (!_refreshTokenStore.ValidateToken(request.RefreshToken, out var username))
            return Unauthorized();

        var newAccessToken = GenerateJwtToken(username);
        var newRefreshToken = GenerateRefreshToken();

        _refreshTokenStore.StoreToken(newRefreshToken, username);
        _refreshTokenStore.RevokeToken(request.RefreshToken);

        return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }

    private string GenerateJwtToken(string username)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            
        };

        if (username == "test")
            claims.Add(new Claim("roles", "User"));

        else if (username == "admin")
            claims.Add(new Claim("roles", "Admin"));


        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
