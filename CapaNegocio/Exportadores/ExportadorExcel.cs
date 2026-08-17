using CapaNegocio.DTOs.DTOLectura;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Exportadores
{
    public class ExportadorExcel : IExportador
    {
        public byte[] Exportar(ReporteDTO reporte)
        {
            using var workbook = new XLWorkbook();
            var hoja = workbook.Worksheets.Add("Reporte");

            hoja.Cell(1, 1).Value = $"Reporte de {reporte.Mes}/{reporte.Anio}";
            hoja.Cell(2, 1).Value = "Total gastado:";
            hoja.Cell(2, 2).Value = reporte.TotalGastado;
            hoja.Cell(3, 1).Value = "Total mes anterior:";
            hoja.Cell(3, 2).Value = reporte.TotalMesAnterior;

            hoja.Cell(5, 1).Value = "Categoria";
            hoja.Cell(5, 2).Value = "Monto";

            int fila = 6;
            foreach (var item in reporte.DesglosePorCategoria)
            {
                hoja.Cell(fila, 1).Value = item.NombreCategoria;
                hoja.Cell(fila, 2).Value = item.MontoTotal;
                fila++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        
        }
    }
}
