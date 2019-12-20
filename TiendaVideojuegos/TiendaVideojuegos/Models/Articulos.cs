using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Articulos
    {
        [Required]
        public Guid IdUnidad { get; set; }
        [Required]
        public Guid IdProducto { get; set; }
        [Required]
        public Productos Producto { get; set; }
        [Required]
        public bool Vendido { get; set; }
        public ArticulosNuevosAbastecimiento ArticuloNuevoAbastecimiento { get; set; }
        public ArticulosSegundaManoReventa ArticuloSegundaManoReventa { get; set; }
        public Ventas Venta { get; set; }
    }
}
