using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Proveedores
    {
        public Guid IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string EnlaceCatalogo { get; set; }
    }
}
