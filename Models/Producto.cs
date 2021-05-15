using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        [Key]
        public int IdProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        public int IdCategoria { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        [InverseProperty(nameof(Categoria.Productos))]
        public virtual Categoria IdCategoriaNavigation { get; set; }
        [InverseProperty(nameof(DetalleCompra.IdProductoNavigation))]
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        [InverseProperty(nameof(DetalleVentum.IdProductoNavigation))]
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
