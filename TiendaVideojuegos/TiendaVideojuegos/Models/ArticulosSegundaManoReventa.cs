using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class ArticulosSegundaManoReventa
    {
        [Required]
        public Guid IdUnidad { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public Guid IdAbonado { get; set; }

        [Required]
        public Articulos Articulo { get; set; }

        [Required]
        public Abonados Abonado { get; set; }
    }
}
