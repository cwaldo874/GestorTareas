using Azure.Core;
using BKGestionTareas.DataAccess;
using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BKGestionTareas.Repository
{
    public class LoginRepository:ILoginRepository
    {
        private readonly UsuarioDbContext _context;
        private Usuario usuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginRepository(UsuarioDbContext context, IHttpContextAccessor httpContextAccessor)
        { 
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool VerifyUserExist(DtoUsuario dtoUsuario)
        {
            usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(t => t.Correo == dtoUsuario.Email && t.Clave == dtoUsuario.Password);
            if (usuario == null)
                return false;
            return true;
        }

        public string CreateToken(DtoUsuario dtoUsuario, string secretKey)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);
            return tokencreado;
        }

        public int GetUserByToken()
        {
            int idUsario = 0;
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var claims = jwtToken.Claims;
                var result = claims.Select(t => t).Where(t => t.Type == "nameid").FirstOrDefault();
                if (result != null)
                {
                    idUsario = Int32.Parse(result.Value);

                }
            }
            return idUsario;
        }



    }
}
