using BKGestionTareas.DataAccess;
using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BKGestionTareas.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly TareaDbContext _context;
        public CategoriaRepository(TareaDbContext context) => _context = context;

        public async Task<DtoCategoria> CreateCategoryAsync(DtoCategoria dtoCategoria)
        {
            Categoria categoria = GetDtoCategoryToCategory(dtoCategoria);
            await _context.Set<Categoria>().AddAsync(categoria);
            await _context.SaveChangesAsync();
            return dtoCategoria;
        }

        public async Task<bool> DeleteCategoryAsync(DtoCategoria dtoCategoria)
        {
            Categoria categoria = GetDtoCategoryToCategory(dtoCategoria);
            if (categoria is null)
            {
                return false;
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return true;
        }

        public DtoCategoria GetCategoryById(int id)
        {
            Categoria categoria = _context.Categorias.Find(id);
            var dtoCategoria = GetDtoCategoria(categoria);
            return dtoCategoria;
        }

        public IEnumerable<DtoCategoria> GetCategories()
        {
            List<Categoria> categorias = _context.Categorias.ToList();
            List<DtoCategoria> dtoCategorias = new List<DtoCategoria>();
            foreach (var categoria in categorias)
            {
                var dtoCategoria = GetDtoCategoria(categoria);
                dtoCategorias.Add(dtoCategoria);
            }
            return dtoCategorias;
        }

        public async Task<bool> UpdateCategoryAsync(DtoCategoria dtoCategoria)
        {
            _context.Entry(GetDtoCategoryToCategory(dtoCategoria)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        private Categoria GetDtoCategoryToCategory(DtoCategoria dtoCategoria)
        {
            var categoria = new Categoria
            {
                Id = dtoCategoria.Id,
                Nombre = dtoCategoria.Nombre
            };
            return categoria;
        }
        private DtoCategoria GetDtoCategoria(Categoria categoria)
        {
            var dtoCategoria = new DtoCategoria
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
            return dtoCategoria;
        }
    }
}
