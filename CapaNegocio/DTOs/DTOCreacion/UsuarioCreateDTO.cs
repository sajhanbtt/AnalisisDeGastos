using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOCreacion
{
    public class UsuarioCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]
        public string Clave { get; set; }

    }
}
