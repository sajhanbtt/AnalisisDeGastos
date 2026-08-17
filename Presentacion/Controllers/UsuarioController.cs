using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("Usuarios")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar(UsuarioUpdateDTO dto)
        {
            var idUsuario = ObtenerUsuarioPorId();

            await _service.Actualizar(idUsuario, dto);

            return Ok();
        }

        private int ObtenerUsuarioPorId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

    }
}
