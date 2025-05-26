using Microsoft.AspNetCore.Http;
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
    public class PerfilController : ControllerBase
    {
        private readonly PerfilServices _PerfilService;
        public PerfilController(PerfilServices perfilService)
        {
            _PerfilService = perfilService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Get()
        {
            return Ok(await _PerfilService.lista());
        }


        [HttpPost]
        [Route("guardar")]

        public async Task<ActionResult> Guardar([FromBody] PerfilDTO perfilDTO)
        {
            await _PerfilService.Guardar(perfilDTO);
            return Ok("perfil Agregado");
        }

        [HttpPut]
        [Route("editar")]

        public async Task<ActionResult> Editar([FromBody] PerfilDTO perfilDTO)
        {
            var editado = await _PerfilService.Editar(perfilDTO);
            if (!editado) return NotFound("Perfil no ecnontrado");

            return Ok("Perfil Modificado");
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _PerfilService.Eliminar(id);
            if (!eliminado) return NotFound("perfil No encontrado");

            return Ok("Perfil Eliminado");
        }
    }
}
