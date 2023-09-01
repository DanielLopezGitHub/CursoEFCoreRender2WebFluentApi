using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEFCore.Migrations
{
    public partial class RenombrarTablaYColumnaArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos");

            migrationBuilder.RenameTable(
                name: "Articulos",
                newName: "Tbl_Articulo");

            migrationBuilder.RenameColumn(
                name: "TituloArticulo",
                table: "Tbl_Articulo",
                newName: "Titulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_Articulo",
                table: "Tbl_Articulo",
                column: "ArticuloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_Articulo",
                table: "Tbl_Articulo");

            migrationBuilder.RenameTable(
                name: "Tbl_Articulo",
                newName: "Articulos");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Articulos",
                newName: "TituloArticulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos",
                column: "ArticuloId");
        }
    }
}
