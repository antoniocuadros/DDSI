using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaVideojuegos.Migrations
{
    public partial class Logueado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Logueado",
                table: "Abonados",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logueado",
                table: "Abonados");
        }
    }
}
