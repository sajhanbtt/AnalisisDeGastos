using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Excepciones
{
    public class ValidacionException : Exception
    {
        public ValidacionException(string message) : base(message)
        {

        }
    }
}
