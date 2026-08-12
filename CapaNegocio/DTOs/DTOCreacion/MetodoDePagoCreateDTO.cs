using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOCreacion
{
    public class MetodoDePagoCreateDTO
    {
        [Required]
        public string NombreMetodo { get; set; }
        public string Descripcion { get; set; }
    }
}
