using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        [Key]
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        [Required]
        [StringLength(15)]
        public string NumeroFactura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.Venta))]
        public virtual Cliente IdClienteNavigation { get; set; }
        [InverseProperty(nameof(DetalleVentum.IdVentaNavigation))]
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
