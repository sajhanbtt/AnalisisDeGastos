using CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaNegocio.DTOs.DTOLectura
{
    public class PresupuestoDTO
    {

        public int Id { get; set; }
        public int Mes { get; set; }

        public int Anio { get; set; }

        public decimal Monto { get; set; }
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

    }
}
