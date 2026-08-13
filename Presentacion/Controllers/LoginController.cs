using CapaDatos.Repositorios.Interfaces;
using CapaNegocio.DTOs.Auth;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Excepciones;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost("Registrar")]
        public async Task <IActionResult> Registrar(UsuarioCreateDTO dto)
        {
            var user = await _service.Registrar(dto);
            return CreatedAtAction(nameof(Registrar), new {id = user.Id}, user);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _service.Login(dto);
            return Ok(user);

        }

    }
}
