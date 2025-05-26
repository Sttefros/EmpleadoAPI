using Microsoft.AspNetCore.Mvc;
using CrudApi.Entities;
using CrudApi.DTOs;

using Microsoft.EntityFrameworkCore;
using CrudApi.Properties.Context;
using CrudApi.Services;


namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoServices _EmpleadoService;
        public EmpleadoController(EmpleadoServices empleadoService)
        {
            _EmpleadoService = empleadoService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
        {
            return Ok(await _EmpleadoService.lista());
        }


        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id)
        {
            var empleado = await _EmpleadoService.Buscar(id);

            if (empleado == null)
                return NotFound("Empleado no encontrado");

            return Ok(empleado);

        }

        [HttpPost]
        [Route("guardar")]

        public async Task<ActionResult> Guardar([FromBody] EmpleadoDTO empleadoDTO)
        {

            await _EmpleadoService.Guardar(empleadoDTO);
            return Ok("Empleado Agregado");
        }


        [HttpPut]
        [Route("editar")]

        public async Task<ActionResult> Editar([FromBody]  EmpleadoDTO empleadoDTO)
        {
            var editado = await _EmpleadoService.Editar(empleadoDTO);
            if (!editado) return NotFound("Empleado no encontrado");

            return Ok("Empleado Modificado");
        }


        [HttpDelete]
        [Route("eliminar/{id}")]

        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _EmpleadoService.Eliminar(id);
            if (!eliminado) return NotFound("Empleado no encontrado");

            return Ok("Empleado Elimiado");
        }
    }
}
