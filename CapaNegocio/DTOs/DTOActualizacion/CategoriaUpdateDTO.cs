using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class CategoriaUpdateDTO
    {
     
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
