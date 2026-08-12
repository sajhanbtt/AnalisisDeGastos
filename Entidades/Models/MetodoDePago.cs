using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaEntidades.Models
{
    public class MetodoDePago
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreMetodo { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; } = true;
        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

    }
}
