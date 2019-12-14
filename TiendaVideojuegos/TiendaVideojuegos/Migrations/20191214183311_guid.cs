using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaVideojuegos.Migrations
{
    public partial class guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonados",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    DNI = table.Column<string>(nullable: true),
                    Contraseña = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    e_mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonados", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonados");
        }
    }
}
