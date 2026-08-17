using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CapaNegocio.Exportadores
{
    public class ExportadorJson : IExportador
    {
        public byte[] Exportar(ReporteDTO reporte)
        {
            var json = JsonSerializer.Serialize(reporte);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
