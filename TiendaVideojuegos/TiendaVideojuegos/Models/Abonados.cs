using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaVideojuegos.Models
{
    public class Abonados
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public string e_mail { get; set; }
    }
}
