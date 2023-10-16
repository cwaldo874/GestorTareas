using BKGestionTareas.DataAccess;
using BKGestionTareas.Models;

namespace BKGestionTareas.Services
{
    public class UserService
    {
        private readonly UsuarioDbContext _context; 

        public UserService(UsuarioDbContext context)
        {
            _context = context;
        }

        public bool VerifyCredentials(string username, string password)
        {
            var user = _context.usuarioSeguridads.FirstOrDefault(u => u.NombreUsuario == username);
            if (user != null && VerifyPassword(password, user))
            {
                return true;
            }
            return false;
        }

        private bool VerifyPassword(string password, UsuarioSeguridad usuarioSeguridad)
        {
            if (usuarioSeguridad != null)
            {
                if (usuarioSeguridad.Contraseña != password)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
