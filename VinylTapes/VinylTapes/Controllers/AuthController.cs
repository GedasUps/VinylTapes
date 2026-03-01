using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VinylTapes.Data;
using VinylTapes.Models;
using VinylTapes.Services;
using VinylTapes.Shared.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DiscogsAuthService _authService;
    // Laikinai saugome Secret atmintyje (tik testavimui!)
    private static string? _tempSecret;
    private readonly VinylDbContext _context;
    private readonly IConfiguration _config;
    public AuthController(DiscogsAuthService authService, VinylDbContext context, IConfiguration config)
    {
        _authService = authService;
        _context = context;
        _config = config;
    }

    [HttpGet("Discogs_login")]
    public async Task<IActionResult> DisgocsLogin()
    {
        
        return Redirect($"https://www.discogs.com/oauth/authorize?oauth_token=aaa");
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string oauth_token, [FromQuery] string oauth_verifier)
    {
        var (accessToken, accessSecret) = await _authService.GetAccessTokenAsync(oauth_verifier, oauth_token, _tempSecret!);
        return Ok(new { AccessToken = accessToken, AccessSecret = accessSecret });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (await _context.Naudotojais.AnyAsync(u => u.ElPastas == model.ElPastas))
        {
            return BadRequest("Naudotojas su tokiu el. paštu jau egzistuoja.");
        }
        var (token, secret) = await _authService.GetRequestTokenAsync();
        var naudotojas = new Naudotojai
        {
            ElPastas = model.ElPastas,
            Vardas = model.Vardas,
            Slaptazodis = BCrypt.Net.BCrypt.HashPassword(model.Slaptazodis),
            Role = 2, 
            DisgocsZetonas=token,
            DiscogsPaslaptis= secret
        };

        _context.Naudotojais.Add(naudotojas);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registracija sėkminga" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var user = await _context.Naudotojais
            .Include(u => u.RoleNavigation)
            .FirstOrDefaultAsync(u => u.ElPastas == model.ElPastas);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Slaptazodis, user.Slaptazodis))
        {
            return Unauthorized("Neteisingas el. paštas arba slaptažodis.");
        }

        var token = GenerateJwtToken(user);
        return Ok(new { token = token, email = user.ElPastas });
    }

    private string GenerateJwtToken(Naudotojai user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.ElPastas),
            new Claim(ClaimTypes.Role, user.RoleNavigation ?.Name ?? "Naudotojas"),
            new Claim("Vardas", user.Vardas)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}