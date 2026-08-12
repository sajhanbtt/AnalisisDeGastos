using CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaNegocio.DTOs.DTOCreacion
{
    public class GastoCreateDTO
    {
        [Required]
        public decimal Monto { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        public int IdMetodoPago { get; set; }
       
    }
}
