using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVideojuegos.Models;

namespace TiendaVideojuegos.ViewModels
{
    public class DevolverViewModel
    {
        public List<Ventas> ventas { get; set; }
        public string nombreProducto { get; set; }
        public float PrecioProducto { get; set; }

    }
}
