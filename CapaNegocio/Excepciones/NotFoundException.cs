using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Excepciones
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensaje) : base(mensaje)
        {

        }
    }
}
