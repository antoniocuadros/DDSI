using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.ViewModels
{
    public class ComprarArticulosViewModel
    {
        public Guid IdProducto { get; set; } //mirar get y set
        public Guid IdProveedor { get; set; }
        public int Cantidad { get; set; }

    }
}
