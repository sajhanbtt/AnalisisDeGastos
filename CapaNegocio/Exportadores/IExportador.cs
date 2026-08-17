using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Exportadores
{
    public interface IExportador
    {
        byte[] Exportar(ReporteDTO reporte);
    }
}
