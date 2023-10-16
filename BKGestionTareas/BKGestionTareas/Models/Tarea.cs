namespace BKGestionTareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaTarea { get; set; }
        public bool Completada { get; set; }
        public int CategoriaId { get; set; }
    }
}
