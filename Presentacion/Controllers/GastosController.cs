using CapaEntidades.Models;
using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("Gastos")]
    [ApiController]
    [Authorize]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _service;
        private readonly IExportacionService _exportacionService;

        public GastosController(IGastoService service, IExportacionService exportacionService)
        {
            _service = service;
            _exportacionService = exportacionService;
        }

        [HttpGet("Listar")]
        public async Task <IActionResult> ListarGastos()
        {
            var idUsuario = ObtenerIdUsuario();

            var gastos = await _service.Listar(idUsuario);
            return Ok(gastos);
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task <IActionResult> ObtenerPorId([FromRoute] int id)
        {
            var idUsuario = ObtenerIdUsuario();

            var gasto = await _service.ObtenerPorId(id, idUsuario);
            return Ok(gasto);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear(GastoCreateDTO dto)
        {
            var idUsuario = ObtenerIdUsuario();
            var gasto = await _service.Crear(dto, idUsuario);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = gasto.Id}, gasto);
        }

        [HttpPost("ImportarArchivo")]
        public async Task<IActionResult> ImportarArchivo(IFormFile archivo)
        {
            var idUsuario = ObtenerIdUsuario();
            await _service.ImportarArchivo(archivo, idUsuario);

            return Ok();
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar([FromRoute]int id,GastoUpdateDTO dto)
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

        [HttpGet("Exportar")]
        public async Task<IActionResult> Exportar(int mes, int anio, [FromQuery] string formato)
        {
            var idUsuario = ObtenerIdUsuario();
            var reporte = await _service.ObtenerReporteMensual(mes, anio, idUsuario);
            var archivo = _exportacionService.Exportar(reporte, formato);

            return File(archivo, "application/octet-stream", $"reporte.{formato}");
        }
        private int ObtenerIdUsuario()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}
