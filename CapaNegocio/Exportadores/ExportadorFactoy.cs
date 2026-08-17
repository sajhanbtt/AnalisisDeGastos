using CapaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Exportadores
{
    public class ExportadorFactoy
    {
        public IExportador Crear(string formato)
        {
            return formato.ToLower() switch
            {
                "json" => new ExportadorJson(),
                "txt" => new ExportadorTxto(),
                "excel" => new ExportadorExcel(),
                _ => throw new ValidacionException("Formato no no disponible. Solo: json, txt o excel")
            };
        }

    }
}
