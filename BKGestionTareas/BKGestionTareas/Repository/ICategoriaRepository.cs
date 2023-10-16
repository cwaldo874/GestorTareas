using BKGestionTareas.DTO;

namespace BKGestionTareas.Repository
{
    public interface ICategoriaRepository
    {
        Task<DtoCategoria> CreateCategoryAsync(DtoCategoria dtoCategoria);
        Task<bool> DeleteCategoryAsync(DtoCategoria dtoCategoria);
        DtoCategoria GetCategoryById(int id);
        IEnumerable<DtoCategoria> GetCategories();
        Task<bool> UpdateCategoryAsync(DtoCategoria dtoCategoria);
    }
}
