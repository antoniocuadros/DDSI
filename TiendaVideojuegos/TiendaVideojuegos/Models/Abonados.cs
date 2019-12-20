using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Abonados
    {
        [Required]
        public Guid IdAbonado { get; set; }

        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "El formato del DNI no es valido")]
       // [RegularExpression(@"[0-9]{8}A-Z$", ErrorMessage = "El formato del DNI no es valido")]
        [Required]
        public string DNI { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña ha de tener más de 6 caracteres y menos de 100")]
        public string Contraseña { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [EmailAddress]
        [Required]
        public string e_mail { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public bool Logueado { get; set; }

        public List<ArticulosSegundaManoReventa> ArticulosSegundaManoReventa { get; set; }

        public List<Ventas> Ventas { get; set; }
    }
}
