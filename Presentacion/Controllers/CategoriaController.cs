using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("categoria")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
       
        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var idUsuario = ObtenerIdUsuarioActual();

            var categorias = await _service.Listar(idUsuario);
            return Ok(categorias);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] CategoriaCreateDTO dto)
        {
            var idUsuario = ObtenerIdUsuarioActual();

            var categoria = await _service.Crear(dto,idUsuario);

            return CreatedAtAction(nameof(ObtenerPorId), new {id = categoria.Id}, categoria);
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int id)
        {
            var idUsuario = ObtenerIdUsuarioActual();

            var categoria = await _service.ObtenerPorId(id, idUsuario);
            return Ok(categoria);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar([FromRoute] int id, CategoriaUpdateDTO dto)
        {
            var idUsuario = ObtenerIdUsuarioActual();

            await _service.Actualizar(id, dto, idUsuario);
            return Ok();
        }

         [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id, int? idReasignacion)
        {
            var idUsuario = ObtenerIdUsuarioActual();

            await _service.Eliminar(id, idUsuario, idReasignacion);
            return NoContent();
        }

        protected int ObtenerIdUsuarioActual()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        }
    }
}
