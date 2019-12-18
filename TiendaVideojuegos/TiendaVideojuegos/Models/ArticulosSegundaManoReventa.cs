using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class ArticulosSegundaManoReventa
    {
        public Guid IdUnidad { get; set; }
        public string Estado { get; set; }
        public Guid IdAbonado { get; set; }
        public Articulos Articulo { get; set; }
        public Abonados Abonado { get; set; }
    }
}
