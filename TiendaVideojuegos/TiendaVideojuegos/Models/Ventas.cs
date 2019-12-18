using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Ventas
    {
        [Required]
        public Guid IdUnidad { get; set; }
        public Guid IdAbonado { get; set; }
        public DateTime FechaVenta { get; set; }
        public Abonados Abonado { get; set; }
        public Articulos Articulo { get; set; }
    }
}
