using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class MetodoDePagoUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string NombreMetodo { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [StringLength(100)]
        public string Icono { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
