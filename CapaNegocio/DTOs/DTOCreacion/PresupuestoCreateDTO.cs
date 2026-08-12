using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOCreacion
{
    public class PresupuestoCreateDTO
    {
        [Required]
        public int Mes { get; set; }

        [Required]
        public int Anio { get; set; }

        [Required]
        public decimal Monto { get; set; }
        [Required]
        public int IdCategoria { get; set; }
    }
}
