using CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaNegocio.DTOs.DTOLectura
{
    public class MetodoDePagoDTO
    {
        public int Id { get; set; }
        public string NombreMetodo { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

    }
}
