using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs.DTOLectura
{
    public class UsuarioDTO
    {
        public int Id  { get; set; }
        public string Nombre { get; set; }

        public string Correo {  get; set; }
    }
}
