﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaVideojuegos.Data;

namespace TiendaVideojuegos.Migrations
{
    [DbContext(typeof(AbonadosContext))]
    [Migration("20191220011905_Vendido")]
    partial class Vendido
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TiendaVideojuegos.Models.Abonados", b =>
                {
                    b.Property<Guid>("IdAbonado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Contraseña")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Logueado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("e_mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAbonado");

                    b.ToTable("Abonados");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.Articulos", b =>
                {
                    b.Property<Guid>("IdUnidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdProducto")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Vendido")
                        .HasColumnType("bit");

                    b.HasKey("IdUnidad");

                    b.HasIndex("IdProducto");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.ArticulosNuevosAbastecimiento", b =>
                {
                    b.Property<Guid>("IdUnidad")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdAbastecimiento")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdProveedor")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUnidad");

                    b.HasIndex("IdProveedor");

                    b.ToTable("ArticulosNuevosAbastecimientos");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.ArticulosSegundaManoReventa", b =>
                {
                    b.Property<Guid>("IdUnidad")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdAbonado")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUnidad");

                    b.HasIndex("IdAbonado");

                    b.ToTable("ArticulosSegundaManoReventa");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.Productos", b =>
                {
                    b.Property<Guid>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaLanzamiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Plataforma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.HasKey("IdProducto");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.Proveedores", b =>
                {
                    b.Property<Guid>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EnlaceCatalogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdProveedor");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.Ventas", b =>
                {
                    b.Property<Guid>("IdUnidad")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdAbonado")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUnidad");

                    b.HasIndex("IdAbonado");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.Articulos", b =>
                {
                    b.HasOne("TiendaVideojuegos.Models.Productos", "Producto")
                        .WithMany("Articulos")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.ArticulosNuevosAbastecimiento", b =>
                {
                    b.HasOne("TiendaVideojuegos.Models.Proveedores", "Proveedor")
                        .WithMany("ArticulosNuevosAbastecimiento")
                        .HasForeignKey("IdProveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TiendaVideojuegos.Models.Articulos", "Articulo")
                        .WithOne("ArticuloNuevoAbastecimiento")
                        .HasForeignKey("TiendaVideojuegos.Models.ArticulosNuevosAbastecimiento", "IdUnidad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.ArticulosSegundaManoReventa", b =>
                {
                    b.HasOne("TiendaVideojuegos.Models.Abonados", "Abonado")
                        .WithMany("ArticulosSegundaManoReventa")
                        .HasForeignKey("IdAbonado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TiendaVideojuegos.Models.Articulos", "Articulo")
                        .WithOne("ArticuloSegundaManoReventa")
                        .HasForeignKey("TiendaVideojuegos.Models.ArticulosSegundaManoReventa", "IdUnidad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TiendaVideojuegos.Models.Ventas", b =>
                {
                    b.HasOne("TiendaVideojuegos.Models.Abonados", "Abonado")
                        .WithMany("Ventas")
                        .HasForeignKey("IdAbonado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TiendaVideojuegos.Models.Articulos", "Articulo")
                        .WithOne("Venta")
                        .HasForeignKey("TiendaVideojuegos.Models.Ventas", "IdUnidad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
