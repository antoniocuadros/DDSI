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

        [Required]
        public Guid IdAbonado { get; set; }

        public DateTime FechaVenta { get; set; }

        [Required]
        public Abonados Abonado { get; set; }

        [Required]
        public Articulos Articulo { get; set; }
    }
}
