using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class CategoriaUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public string NombreCategoria { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}
