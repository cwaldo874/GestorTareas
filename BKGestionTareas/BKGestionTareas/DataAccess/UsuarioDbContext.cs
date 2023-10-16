using BKGestionTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace BKGestionTareas.DataAccess
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioSeguridad> usuarioSeguridads { get; set; }
    }
}
