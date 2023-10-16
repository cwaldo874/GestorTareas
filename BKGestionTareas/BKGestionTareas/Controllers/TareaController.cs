using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using BKGestionTareas.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BKGestionTareas.Controllers
{
    [Route("api/tarea")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private ITareaRepository _tareaRepository;

        public TareaController(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }
        // GET: TareaController
        [HttpGet]
        public IEnumerable<DtoTarea> GetTasks()
        {
            return _tareaRepository.GetTasks();
        }
        [HttpGet("{id}")]
        public ActionResult<DtoTarea> GetTasksById(int id)
        {
            var taskByID = _tareaRepository.GetTaskById(id);
            if (taskByID == null)
            {
                return NotFound();
            }
            return taskByID;
        }

        [HttpPost]
        public async Task<ActionResult<DtoTarea>> CreateTask(DtoTarea dtoTarea)
        {
            await _tareaRepository.CreateTasktAsync(dtoTarea);
            return CreatedAtAction(nameof(GetTasksById), new { id = dtoTarea.Id }, dtoTarea);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var tarea = _tareaRepository.GetTaskById(id);
            if (tarea == null)
            {
                return NotFound();
            }
            await _tareaRepository.DeleteTaskAsync(tarea);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DtoTarea>> PutRemitente(int id, DtoTarea dtoTarea)
        {
            if (id != dtoTarea.Id)
            {
                return BadRequest();
            }

            await _tareaRepository.UpdateTaskAsync(dtoTarea);

            return NoContent();
        }

    }
}
