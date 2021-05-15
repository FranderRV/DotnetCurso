using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }

        [Key]
        public int IdProveedor { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [Column("DUI")]
        [StringLength(10)]
        public string Dui { get; set; }
        [Required]
        [Column("NIT")]
        [StringLength(17)]
        public string Nit { get; set; }
        [Required]
        [StringLength(250)]
        public string Correo { get; set; }

        [InverseProperty(nameof(Compra.IdProveedorNavigation))]
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
