using CrudApi.Entities;
using CrudApi.DTOs;
using Microsoft.EntityFrameworkCore;
using CrudApi.Properties.Context;
using Microsoft.AspNetCore.Mvc;



namespace CrudApi.Services
{
    public class PerfilServices
    {
        private readonly AppDbContext _context;
        public PerfilServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PerfilDTO>> lista()
        {
            var listaDTO = new List<PerfilDTO>();
            // var listaPerfil = await _context.Perfiles.ToListAsync();

            foreach (var item in await _context.Perfiles.ToListAsync())
            {
                listaDTO.Add(new PerfilDTO
                {
                    IdPerfil = item.IdPerfil,
                    Nombre = item.Nombre
                });
            }

            return listaDTO;
        }

        public async Task Guardar (PerfilDTO perfilDTO)
        {
            var perfil = new Perfil
            {
                Nombre = perfilDTO.Nombre
            };
            await _context.Perfiles.AddAsync(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Editar(PerfilDTO perfilDTO)
        {
            var perfilDB = await _context.Perfiles.FindAsync(perfilDTO.IdPerfil);
            if (perfilDB == null) return false;

            perfilDB.Nombre = perfilDTO.Nombre;

            _context.Perfiles.Update(perfilDB);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> Eliminar(int id)
        {
            var perfilDB = await _context.Perfiles.FindAsync(id);
            if (perfilDB == null) return false;

            _context.Perfiles.Remove(perfilDB);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
