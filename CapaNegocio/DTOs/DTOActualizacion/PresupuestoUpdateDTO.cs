using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.DTOActualizacion
{
    public class PresupuestoUpdateDTO
    {
     
        public int Mes { get; set; }

        
        public int Anio { get; set; }

      
        public decimal Monto { get; set; }
      
        public int IdCategoria { get; set; }
    }
}
