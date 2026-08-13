using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class PresupuestoUpdateDTO
    {
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }

        [Required]
        public int Anio { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Monto { get; set; }
        [Required]
        public int IdCategoria { get; set; }
    }
}
