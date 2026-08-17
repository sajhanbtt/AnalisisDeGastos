using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs.DTOLectura
{
    public class ReporteDTO
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal TotalGastado { get; set; }
        public decimal TotalMesAnterior { get; set; }
        public decimal DiferenciaConMesAnterior { get; set; }
        public List<DesgloseCategoriaDTO> DesglosePorCategoria { get; set; } = new();
        public List<DesgloseCategoriaDTO> TopCategorias { get; set; } = new();
    }
}

