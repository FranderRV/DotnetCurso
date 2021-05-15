using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        [Key]
        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        [Required]
        [StringLength(15)]
        public string NumeroFactura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [ForeignKey(nameof(IdProveedor))]
        [InverseProperty(nameof(Proveedore.Compras))]
        public virtual Proveedore IdProveedorNavigation { get; set; }
        [InverseProperty(nameof(DetalleCompra.IdCompraNavigation))]
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
