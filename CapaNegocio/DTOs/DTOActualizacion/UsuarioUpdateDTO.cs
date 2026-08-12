using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class UsuarioUpdateDTO
    {
      
        public string Nombre { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        public string Clave { get; set; }
    }
}
