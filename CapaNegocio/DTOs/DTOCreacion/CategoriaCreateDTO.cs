using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOCreacion
{
    public class CategoriaCreateDTO
    {

        [Required]
        [StringLength(50)]
        public string NombreCategoria { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

    }
}
