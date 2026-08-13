using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class GastoUpdateDTO
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Monto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        [Required]
        public int IdMetodoPago { get; set; }
    }
}
