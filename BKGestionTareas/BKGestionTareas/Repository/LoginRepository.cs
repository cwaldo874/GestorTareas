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
        public LoginRepository(UsuarioDbContext context) => _context = context;

        public bool VerifyUserExist(DtoUsuario dtoUsuario)
        {
            Usuario usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(t => t.Correo == dtoUsuario.Email && t.Clave == dtoUsuario.Password);
            if (usuario == null)
                return false;
            return true;
        }

        public string CreateToken(DtoUsuario dtoUsuario, string secretKey)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, dtoUsuario.Email));

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

    }
}
