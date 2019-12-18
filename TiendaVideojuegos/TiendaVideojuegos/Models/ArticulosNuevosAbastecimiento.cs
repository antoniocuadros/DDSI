using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class ArticulosNuevosAbastecimiento
    {
        public Guid IdUnidad { get; set; }
        public Guid IdAbastecimiento { get; set; }
        public Guid IdProveedor { get; set; }
        public Articulos Articulo { get; set; }
        public Proveedores Proveedor { get; set; }
    }
}
