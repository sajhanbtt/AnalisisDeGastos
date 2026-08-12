using CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaNegocio.DTOs.DTOLectura
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

        public int IdMetodoPago { get; set; }
        public string NombreMetodo{ get; set; }

    }
}
