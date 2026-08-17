using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs.DTOLectura
{
    public class ImportacionDTO
    {
        public int FilasExitosas { get; set; }
        public List<string> Errores { get; set; } = new List<string>();
    }
}
