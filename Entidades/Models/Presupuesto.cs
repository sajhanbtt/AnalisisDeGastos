using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CapaEntidades.Models
{
    public class Presupuesto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Mes {  get; set; }

        [Required]
        public int Anio { get; set; }

        [Required]
        public decimal Monto { get; set; }
        [Required]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
