using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaVideojuegos.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaVideojuegos.Data
{
    public class AbonadosContext : DbContext
    {
        public AbonadosContext(DbContextOptions<AbonadosContext> options)
            : base(options)
        {
        }

        public DbSet<Abonados> Abonados { get; set; }
    }
}
