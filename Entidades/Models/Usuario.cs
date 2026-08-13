using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapaEntidades.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]
        public string Clave { get; set; }   
   
        public ICollection<Categoria> Categorias { get; set; }

        public ICollection<MetodoDePago> MetodoDePagos { get; set; }

        public ICollection<Gasto> Gastos { get; set; }

        public ICollection<Presupuesto> Presupuestos { get; set; }
    }
}
