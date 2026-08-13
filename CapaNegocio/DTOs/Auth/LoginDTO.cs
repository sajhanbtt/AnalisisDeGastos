using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaNegocio.DTOs.Auth
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]

        public string Email { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
