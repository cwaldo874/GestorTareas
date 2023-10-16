using BKGestionTareas.DataAccess;
using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BKGestionTareas.Repository
{
    public class TareaRepository : ITareaRepository
    {
        private readonly TareaDbContext _context;
        public TareaRepository(TareaDbContext context) => _context = context;

        public async Task<DtoTarea> CreateTasktAsync(DtoTarea dtoTarea)
        {
            Tarea tarea = GetDtoTaskToTask(dtoTarea);
            await _context.Set<Tarea>().AddAsync(tarea);
            await _context.SaveChangesAsync();
            return dtoTarea;
        }

        public async Task<bool> DeleteTaskAsync(DtoTarea dtoTarea)
        {
            Tarea tarea = GetDtoTaskToTask(dtoTarea);
            if (tarea is null)
            {
                return false;
            }
            _context.Set<Tarea>().Remove(tarea);
            await _context.SaveChangesAsync();

            return true; 
        }

        public DtoTarea GetTaskById(int id)
        {
            Tarea tarea= _context.Tareas.AsNoTracking().FirstOrDefault(t=>t.Id==id);
            var dtoTarea = GetDtoTask(tarea);
            return dtoTarea;
        }

        public IEnumerable<DtoTarea> GetTasks()
        {
            List<Tarea> tareas = _context.Tareas.ToList();
            List<DtoTarea> dtoTareas = new List<DtoTarea>();
            foreach (var tarea in tareas)
            {
                var dtoTarea = GetDtoTask(tarea);
                dtoTareas.Add(dtoTarea);
            }
            return dtoTareas;

        }

        public async Task<bool> UpdateTaskAsync(DtoTarea dtotarea)
        {
            _context.Entry(GetDtoTaskToTask(dtotarea)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        private DtoTarea GetDtoTask(Tarea tarea)
        {
            var dtoTarea = new DtoTarea
            {
                Id = tarea.Id,
                Nombre = tarea.Nombre,
                Descripcion = tarea.Descripcion,
                FechaTarea = tarea.FechaTarea,
                Completada = tarea.Completada,
                Categoria = _context.Categorias.FirstOrDefault(c => c.Id == tarea.CategoriaId)
            };
            return dtoTarea;
        }

        private Tarea GetDtoTaskToTask(DtoTarea dtotarea)
        {
            var tarea = new Tarea
            {
                Id = dtotarea.Id,
                Nombre = dtotarea.Nombre,
                Descripcion = dtotarea.Descripcion,
                FechaTarea = dtotarea.FechaTarea,
                Completada = dtotarea.Completada,
                CategoriaId = dtotarea.Categoria.Id,
            };
            return tarea;
        }
    }
}
