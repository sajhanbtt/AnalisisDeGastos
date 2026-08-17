using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class UsuarioUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Clave { get; set; }
    }
}
