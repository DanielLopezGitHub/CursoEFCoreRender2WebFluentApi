using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEFCore.Migrations
{
    public partial class RelacionCategoriaArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categorias",
                newName: "Categoria_Id");

            migrationBuilder.AddColumn<int>(
                name: "Categoria_Id",
                table: "Tbl_Articulo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Articulo_Categoria_Id",
                table: "Tbl_Articulo",
                column: "Categoria_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Articulo_Categorias_Categoria_Id",
                table: "Tbl_Articulo",
                column: "Categoria_Id",
                principalTable: "Categorias",
                principalColumn: "Categoria_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Articulo_Categorias_Categoria_Id",
                table: "Tbl_Articulo");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Articulo_Categoria_Id",
                table: "Tbl_Articulo");

            migrationBuilder.DropColumn(
                name: "Categoria_Id",
                table: "Tbl_Articulo");

            migrationBuilder.RenameColumn(
                name: "Categoria_Id",
                table: "Categorias",
                newName: "Id");
        }
    }
}
