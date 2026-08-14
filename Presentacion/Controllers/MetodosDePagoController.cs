using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("MetodosDePago")]
    [ApiController]
    [Authorize]
    public class MetodosDePagoController : ControllerBase
    {
        private readonly IMetodoPagoService _service;

        public MetodosDePagoController(IMetodoPagoService service)
        {
            _service = service;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var idUsuario = ObtenerIdUsuario();
            var metodosPago = await _service.Listar(idUsuario);

            return Ok(metodosPago);
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int id)
        {
            var idUsuario = ObtenerIdUsuario();
            var metodoPago = await _service.ObtenerPorId(id, idUsuario);

            return Ok(metodoPago);
        }
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear(MetodoDePagoCreateDTO dto)
        {
            var idUsuario = ObtenerIdUsuario();
            var metodoPago = await _service.Crear(dto, idUsuario);

            return CreatedAtAction(nameof(ObtenerPorId) new { id = metodoPago.Id }, metodoPago);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar([FromRoute] int id, MetodoDePagoUpdateDTO dto)
        {
            var idUsuario = ObtenerIdUsuario();
            await _service.Actualizar(id,dto, idUsuario);
            return Ok();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            var idUsuario = ObtenerIdUsuario();
            await _service.Eliminar(id, idUsuario);

            return NoContent();
        }

        private int ObtenerIdUsuario()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }


    }
}
