using BKGestionTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UsuarioSeguridadController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly UserService _userService;

    public UsuarioSeguridadController(IConfiguration config, UserService userService)
    {
        _config = config;
        _userService = userService;
    }
    [HttpPost("login")]
    public ActionResult Login(string username, string password)
    {
        if (_userService.VerifyCredentials(username, password))
        {
            var secret = _config["AppSettings:Secret"];
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized(); 
        }
    }
}