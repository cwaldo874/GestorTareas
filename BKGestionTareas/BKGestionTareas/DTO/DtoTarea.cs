using BKGestionTareas.Models;

namespace BKGestionTareas.DTO
{
    public class DtoTarea
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaTarea { get; set; }
        public bool Completada { get; set; }
        public Categoria Categoria { get; set; }
    }
}
