using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Articulos
    {
        public Guid IdUnidad { get; set; }
        public Guid IdProducto { get; set; }
        public Productos Producto { get; set; }
        public ArticulosNuevosAbastecimiento ArticuloNuevoAbastecimiento { get; set; }
        public ArticulosSegundaManoReventa ArticuloSegundaManoReventa { get; set; }
        public Ventas Venta { get; set; }
    }
}
