using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Exportadores
{
    public class ExportadorTxto : IExportador
    {
        public byte[] Exportar(ReporteDTO reporte)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Reporte de {reporte.Mes}/{reporte.Anio}");
            sb.AppendLine($"Total gastado: {reporte.TotalGastado}");
            sb.AppendLine($"Total mes anterior: {reporte.TotalMesAnterior}");
            sb.AppendLine();
            sb.AppendLine("Desglose por categoría:");

            foreach (var item in reporte.DesglosePorCategoria)
            {
                sb.AppendLine($"- {item.NombreCategoria}: {item.MontoTotal}");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
