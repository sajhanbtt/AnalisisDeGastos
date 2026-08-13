using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Excepciones
{
    public class NotAuthorizeException : Exception
    {
        public NotAuthorizeException(string mensaje) : base(mensaje)
        {

        }
    }
}
