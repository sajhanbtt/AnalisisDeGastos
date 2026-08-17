using CapaNegocio.DTOs.DTOLectura;
using CapaNegocio.Exportadores;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Services
{
    public class ExportacionService : IExportacionService
    {
        public byte[] Exportar(ReporteDTO reporte, string formato)
        {
            var factory = new ExportadorFactoy();
            var exportador = factory.Crear(formato);

            return exportador.Exportar(reporte);
        }
    }
}
