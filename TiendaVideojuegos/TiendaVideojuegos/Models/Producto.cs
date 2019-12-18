using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Producto
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Plataforma { get; set; }
    }
}
