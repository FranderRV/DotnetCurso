using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _.Models
{
    [Table("DetalleCompra")]
    public partial class DetalleCompra
    {
        [Key]
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }
        [Column("IVA", TypeName = "money")]
        public decimal Iva { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [ForeignKey(nameof(IdCompra))]
        [InverseProperty(nameof(Compra.DetalleCompras))]
        public virtual Compra IdCompraNavigation { get; set; }
        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.DetalleCompras))]
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
