using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class ArticulosNuevosAbastecimiento
    {
        [Required]
        public Guid IdUnidad { get; set; }
        [Required]
        public Guid IdAbastecimiento { get; set; }
        [Required]
        public Guid IdProveedor { get; set; }
        [Required]
        public Articulos Articulo { get; set; }
        [Required]
        public Proveedores Proveedor { get; set; }
    }
}
