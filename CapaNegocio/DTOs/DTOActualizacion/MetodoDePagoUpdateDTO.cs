using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class MetodoDePagoUpdateDTO
    {

        public string NombreMetodo { get; set; }
        public string Descripcion { get; set; }
    }
}
