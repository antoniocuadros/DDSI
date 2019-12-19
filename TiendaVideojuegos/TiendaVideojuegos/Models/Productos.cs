using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Productos
    {
        [Required]
        public Guid IdProducto { get; set; }

        [StringLength(200, ErrorMessage = "Ha introducido un nombre demasiado largo")]
        public string Nombre { get; set; }

        [Range(0, 999.99, ErrorMessage = "El precio introducido no es válido")]
        public float Precio { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }

        [Required]
        public string Plataforma { get; set; }

        public List<Articulos> Articulos { get; set; }

        public string Imagen { get; set; }
    }
}
