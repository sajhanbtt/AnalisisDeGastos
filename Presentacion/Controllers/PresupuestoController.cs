using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route ("Presupuestos")]
    [ApiController]
    [Authorize]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _service;

        public PresupuestoController(IPresupuestoService service)
        {
            _service = service; 
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var idUsuario = ObtenerUsuarioPorId();

            var lista = await _service.Listar(idUsuario);

            return Ok(lista);
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int id)
        {
            var idUsuario = ObtenerUsuarioPorId();

            var presupuesto = await _service.ObtenerPorId(id, idUsuario);

            return Ok(presupuesto);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] PresupuestoCreateDTO dto)
        {
            var idUsuario = ObtenerUsuarioPorId();

            var presupuesto = await _service.Crear(dto, idUsuario);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = presupuesto.Id }, presupuesto);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(PresupuestoUpdateDTO dto, [FromRoute] int id)
        {
            var idUsuario = ObtenerUsuarioPorId();
            await _service.Actualizar(id, dto, idUsuario);
            return Ok();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar ([FromRoute] int id)
        {
            var idUsuario = ObtenerUsuarioPorId();
            await _service.Eliminar(id, idUsuario);
            return NoContent();
        }

        private int ObtenerUsuarioPorId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

    }
}
