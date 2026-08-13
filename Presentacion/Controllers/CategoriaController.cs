using CapaNegocio.DTOs.DTOActualizacion;
using CapaNegocio.DTOs.DTOCreacion;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
       
        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _service.Listar();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaCreateDTO dto)
        {
            await _service.Crear(dto);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            await _service.ObtenerPorId(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, CategoriaUpdateDTO dto)
        {
            await _service.Actualizar(id, dto);
            return Ok();
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.Eliminar(id);
            return NoContent();
        }


    }
}
