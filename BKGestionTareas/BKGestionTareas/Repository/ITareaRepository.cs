using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace BKGestionTareas.Repository
{
    public interface ITareaRepository
    {
        Task<DtoTarea> CreateTasktAsync(DtoTarea dtoTarea);
        Task<bool> DeleteTaskAsync(DtoTarea dtoTarea);
        DtoTarea GetTaskById(int id);
        IEnumerable<DtoTarea> GetTasks();
        Task<bool> UpdateTaskAsync(DtoTarea dtoTarea);
    }
}
