using BKGestionTareas.DataAccess;
using BKGestionTareas.DataAccess.Data;
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
        //public CategoriaRepository()
        //{

        //    DbInitializer.Initializer();
        //    using (var context = new TareaDbContext())
        //    {

        //        var categoria = new List<Categoria>
        //        { new Categoria {Id = 1, Nombre = "desarrollo"},
        //         new Categoria {Id = 2, Nombre = "pruebas"}
        //        };
        //        context.Categorias.AddRange(categoria);
        //        context.SaveChanges();

        //    }
        //}
        public async Task<DtoCategoria> CreateCategoryAsync(DtoCategoria dtoCategoria)
        {
            //using (var context = new TareaDbContext())
            //{
            //    Categoria categoriaCrear = GetDtoCategoryToCategory(dtoCategoria);
            //    await context.Set<Categoria>().AddAsync(categoriaCrear);
            //    await context.SaveChangesAsync();
            //    return dtoCategoria;
            //}
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
            //using (var context = new TareaDbContext())
            //{
            //    Categoria categoriaUnica = context.Categorias.Find(id);
            //    var dtoCategoria = GetDtoCategoria(categoriaUnica);
            //    return dtoCategoria;

            //}
            Categoria categoria = _context.Categorias.Find(id);
            var dtoCategoria = GetDtoCategoria(categoria);
            return dtoCategoria;
        }

        public IEnumerable<DtoCategoria> GetCategories()
        {
            //using (var context = new TareaDbContext())
            //{
            //    List<Categoria> categorias = context.Categorias.ToList();
            //    List<DtoCategoria> dtoCategorias = new List<DtoCategoria>();
            //    foreach (var categoria in categorias)
            //    {
            //        var dtoCategoria = GetDtoCategoria(categoria);
            //        dtoCategorias.Add(dtoCategoria);
            //    }
            //    return dtoCategorias;
            //}
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
