using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [Column("Categoria")]
        [StringLength(50)]
        public string Nombre { get; set; }

        public static explicit operator Categoria(Producto v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Categoria(string v)
        {
            throw new NotImplementedException();
        }

        [InverseProperty(nameof(Producto.IdCategoriaNavigation))]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
