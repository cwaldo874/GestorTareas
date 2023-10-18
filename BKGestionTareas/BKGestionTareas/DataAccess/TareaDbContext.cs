using BKGestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BKGestionTareas.DataAccess
{
    public class TareaDbContext : DbContext
    {
        public TareaDbContext(DbContextOptions<TareaDbContext> options) : base(options) { }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    
    }
}
