using CapaNegocio.DTOs.DTOLectura;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IExportacionService
    {
        byte[] Exportar(ReporteDTO reporte, string formato);
    }
}
