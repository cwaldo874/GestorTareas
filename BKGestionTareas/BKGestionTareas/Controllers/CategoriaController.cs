using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using BKGestionTareas.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BKGestionTareas.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    [Authorize]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        [HttpGet]
        public IEnumerable<DtoCategoria> GetCategories()
        {
            return _categoriaRepository.GetCategories();
        }
        [HttpGet("{id}")]
        public ActionResult<DtoCategoria> GetCategoryById(int id)
        {
            var CategoryByID = _categoriaRepository.GetCategoryById(id);
            if (CategoryByID == null)
            {
                return NotFound();
            }
            return CategoryByID;
        }

        [HttpPost]
        public async Task<ActionResult<DtoCategoria>> CreateCategory(DtoCategoria dtoCategoria)
        {
            await _categoriaRepository.CreateCategoryAsync(dtoCategoria);
            return CreatedAtAction(nameof(GetCategoryById), new { id = dtoCategoria.Id }, dtoCategoria);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoria = _categoriaRepository.GetCategoryById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            await _categoriaRepository.DeleteCategoryAsync(categoria);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DtoCategoria>> PutRemitente(int id, DtoCategoria dtoCategoria)
        {
            if (id != dtoCategoria.Id)
            {
                return BadRequest();
            }

            await _categoriaRepository.UpdateCategoryAsync(dtoCategoria);

            return NoContent();
        }
    }
}
