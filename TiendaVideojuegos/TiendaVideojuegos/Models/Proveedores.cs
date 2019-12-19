using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Proveedores
    {
        [Required]
        public Guid IdProveedor { get; set; }
        [StringLength(200, ErrorMessage = "Ha introducido un nombre demasiado largo")]

        public string Nombre { get; set; }

        [Url]
        public string EnlaceCatalogo { get; set; }

        public List<ArticulosNuevosAbastecimiento> ArticulosNuevosAbastecimiento { get; set; }
    }
}
