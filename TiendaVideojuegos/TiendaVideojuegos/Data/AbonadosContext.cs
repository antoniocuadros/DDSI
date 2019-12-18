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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuracion de productos

            modelBuilder.Entity<Productos>()
                .HasKey(b => b.IdProducto);

            modelBuilder.Entity<Productos>()
                .HasMany(b => b.Articulos)
                .WithOne(i => i.Producto)
                .HasForeignKey(b => b.IdProducto);

            // Configuracion de artículos

            modelBuilder.Entity<Articulos>()
                .HasKey(b => b.IdUnidad);

            //modelBuilder.Entity<Articulos>()
            //   .HasOne(b => b.Producto)
            //   .WithMany(i => i.Articulos)
            //   .HasForeignKey(b => b.IdProducto);

            modelBuilder.Entity<Articulos>()
               .HasOne(b => b.ArticuloNuevoAbastecimiento)
               .WithOne(i => i.Articulo)
               .HasForeignKey<ArticulosNuevosAbastecimiento>(b => b.IdUnidad);

            modelBuilder.Entity<Articulos>()
               .HasOne(b => b.ArticuloSegundaManoReventa)
               .WithOne(i => i.Articulo)
               .HasForeignKey<ArticulosSegundaManoReventa>(b => b.IdUnidad);

            modelBuilder.Entity<Articulos>()
               .HasOne(b => b.Venta)
               .WithOne(i => i.Articulo)
               .HasForeignKey<Ventas>(b => b.IdUnidad);

            //configuracion de ArticulosNuevosAbastecimiento

            modelBuilder.Entity<ArticulosNuevosAbastecimiento>()
                .HasKey(b => b.IdUnidad);

            //configuracion de ArticulosSegundaManoReventa

            modelBuilder.Entity<ArticulosSegundaManoReventa>()
                .HasKey(b => b.IdUnidad);

            //configuracion de proveedores

            modelBuilder.Entity<Proveedores>()
                .HasKey(b => b.IdProveedor);

            modelBuilder.Entity<Proveedores>()
               .HasMany(b => b.ArticulosNuevosAbastecimiento)
               .WithOne(i => i.Proveedor)
               .HasForeignKey(b => b.IdProveedor);

            //Configuracion de Abonados

            modelBuilder.Entity<Abonados>()
                .HasKey(b => b.IdAbonado);

            modelBuilder.Entity<Abonados>()
                .HasMany(b => b.ArticulosSegundaManoReventa)
                .WithOne(i => i.Abonado)
                .HasForeignKey(b => b.IdAbonado);

            modelBuilder.Entity<Abonados>()
                .HasMany(b => b.Ventas)
                .WithOne(i => i.Abonado)
                .HasForeignKey(b => b.IdAbonado);

            // Configuracion Ventas

            modelBuilder.Entity<Ventas>()
                .HasKey(b => b.IdUnidad);


        }

        public DbSet<Abonados> Abonados { get; set; }
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<ArticulosNuevosAbastecimiento> ArticulosNuevosAbastecimientos { get; set; }
        public DbSet<ArticulosSegundaManoReventa> ArticulosSegundaManoReventa { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Ventas> Ventas { get; set; }

    }
}
