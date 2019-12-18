using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Venta
    {
        public Guid IdUnidad { get; set; }
        public Guid IdAbonado { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}
