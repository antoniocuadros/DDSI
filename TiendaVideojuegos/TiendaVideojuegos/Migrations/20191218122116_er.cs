using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaVideojuegos.Migrations
{
    public partial class er : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Abonados",
                table: "Abonados");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Abonados");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAbonado",
                table: "Abonados",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Abonados",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abonados",
                table: "Abonados",
                column: "IdAbonado");

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Precio = table.Column<float>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaLanzamiento = table.Column<DateTime>(nullable: false),
                    Plataforma = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    EnlaceCatalogo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    IdUnidad = table.Column<Guid>(nullable: false),
                    IdProducto = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.IdUnidad);
                    table.ForeignKey(
                        name: "FK_Articulos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticulosNuevosAbastecimientos",
                columns: table => new
                {
                    IdUnidad = table.Column<Guid>(nullable: false),
                    IdAbastecimiento = table.Column<Guid>(nullable: false),
                    IdProveedor = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulosNuevosAbastecimientos", x => x.IdUnidad);
                    table.ForeignKey(
                        name: "FK_ArticulosNuevosAbastecimientos_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticulosNuevosAbastecimientos_Articulos_IdUnidad",
                        column: x => x.IdUnidad,
                        principalTable: "Articulos",
                        principalColumn: "IdUnidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticulosSegundaManoReventa",
                columns: table => new
                {
                    IdUnidad = table.Column<Guid>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    IdAbonado = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulosSegundaManoReventa", x => x.IdUnidad);
                    table.ForeignKey(
                        name: "FK_ArticulosSegundaManoReventa_Abonados_IdAbonado",
                        column: x => x.IdAbonado,
                        principalTable: "Abonados",
                        principalColumn: "IdAbonado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticulosSegundaManoReventa_Articulos_IdUnidad",
                        column: x => x.IdUnidad,
                        principalTable: "Articulos",
                        principalColumn: "IdUnidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    IdUnidad = table.Column<Guid>(nullable: false),
                    IdAbonado = table.Column<Guid>(nullable: false),
                    FechaVenta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.IdUnidad);
                    table.ForeignKey(
                        name: "FK_Ventas_Abonados_IdAbonado",
                        column: x => x.IdAbonado,
                        principalTable: "Abonados",
                        principalColumn: "IdAbonado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Articulos_IdUnidad",
                        column: x => x.IdUnidad,
                        principalTable: "Articulos",
                        principalColumn: "IdUnidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_IdProducto",
                table: "Articulos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosNuevosAbastecimientos_IdProveedor",
                table: "ArticulosNuevosAbastecimientos",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosSegundaManoReventa_IdAbonado",
                table: "ArticulosSegundaManoReventa",
                column: "IdAbonado");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdAbonado",
                table: "Ventas",
                column: "IdAbonado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticulosNuevosAbastecimientos");

            migrationBuilder.DropTable(
                name: "ArticulosSegundaManoReventa");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abonados",
                table: "Abonados");

            migrationBuilder.DropColumn(
                name: "IdAbonado",
                table: "Abonados");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Abonados");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Abonados",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abonados",
                table: "Abonados",
                column: "Id");
        }
    }
}
