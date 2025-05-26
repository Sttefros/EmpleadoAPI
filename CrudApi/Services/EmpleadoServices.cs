using System.Threading.Tasks;
using CrudApi.DTOs;
using CrudApi.Entities;
using CrudApi.Properties.Context;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Services
{
    public class EmpleadoServices
    {
        private readonly AppDbContext _context;

        public EmpleadoServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDTO>> lista()
        {
            var empleados = await _context.Empleados
            .Include(e => e.PerfilReferencia) // <- esto es lo clave
            .ToListAsync();
            var listaDTO = new List<EmpleadoDTO>();

            foreach (var item in await _context.Empleados.ToListAsync())
            {
                listaDTO.Add(new EmpleadoDTO
                {
                    IdEmpleado = item.IdEmpleado,
                    Nombre = item.Nombre,
                    Sueldo = item.Sueldo,
                    IdPerfil = item.IdPerfil,
                    NombrePerfil = item.PerfilReferencia?.Nombre

                });
            }

            return listaDTO;
        }

        public async Task<EmpleadoDTO?> Buscar(int id)
        {
            var empleadoDB = await _context.Empleados.Include(p => p.PerfilReferencia)
                .Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();

            if (empleadoDB == null) return null;

            return new EmpleadoDTO
            {
                IdEmpleado = empleadoDB.IdEmpleado,
                Nombre = empleadoDB.Nombre,
                Sueldo = empleadoDB.Sueldo,
                IdPerfil = empleadoDB.IdPerfil,
                NombrePerfil = empleadoDB.PerfilReferencia.Nombre
            };
        }


        public async Task Guardar (EmpleadoDTO empleadoDTO)
        {
            var empleado = new Empleado
            {
                Nombre = empleadoDTO.Nombre,
                Sueldo = empleadoDTO.Sueldo,
                IdPerfil = empleadoDTO.IdPerfil
            };

            await _context.Empleados.AddAsync(empleado);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Editar(EmpleadoDTO empleadoDTO){
            var empleadoDB = await _context.Empleados.FindAsync(empleadoDTO.IdEmpleado);
            if(empleadoDB == null) return false;

            empleadoDB.Nombre = empleadoDTO.Nombre;
            empleadoDB.Sueldo = empleadoDTO.Sueldo;
            empleadoDB.IdPerfil = empleadoDTO.IdPerfil;

            _context.Empleados.Update(empleadoDB);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> Eliminar(int id)
        {
            var empleadoDB = await _context.Empleados.FindAsync(id);
            if (empleadoDB == null) return false;

            _context.Empleados.Remove(empleadoDB);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
