using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEFCore.Migrations
{
    public partial class ArticuloEtiquetasDBSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloEtiqueta_Etiquetas_Etiqueta_Id",
                table: "ArticuloEtiqueta");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloEtiqueta_Tbl_Articulo_Articulo_Id",
                table: "ArticuloEtiqueta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticuloEtiqueta",
                table: "ArticuloEtiqueta");

            migrationBuilder.RenameTable(
                name: "ArticuloEtiqueta",
                newName: "ArticuloEtiquetas");

            migrationBuilder.RenameIndex(
                name: "IX_ArticuloEtiqueta_Etiqueta_Id",
                table: "ArticuloEtiquetas",
                newName: "IX_ArticuloEtiquetas_Etiqueta_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticuloEtiquetas",
                table: "ArticuloEtiquetas",
                columns: new[] { "Articulo_Id", "Etiqueta_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloEtiquetas_Etiquetas_Etiqueta_Id",
                table: "ArticuloEtiquetas",
                column: "Etiqueta_Id",
                principalTable: "Etiquetas",
                principalColumn: "Etiqueta_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloEtiquetas_Tbl_Articulo_Articulo_Id",
                table: "ArticuloEtiquetas",
                column: "Articulo_Id",
                principalTable: "Tbl_Articulo",
                principalColumn: "Articulo_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloEtiquetas_Etiquetas_Etiqueta_Id",
                table: "ArticuloEtiquetas");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloEtiquetas_Tbl_Articulo_Articulo_Id",
                table: "ArticuloEtiquetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticuloEtiquetas",
                table: "ArticuloEtiquetas");

            migrationBuilder.RenameTable(
                name: "ArticuloEtiquetas",
                newName: "ArticuloEtiqueta");

            migrationBuilder.RenameIndex(
                name: "IX_ArticuloEtiquetas_Etiqueta_Id",
                table: "ArticuloEtiqueta",
                newName: "IX_ArticuloEtiqueta_Etiqueta_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticuloEtiqueta",
                table: "ArticuloEtiqueta",
                columns: new[] { "Articulo_Id", "Etiqueta_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloEtiqueta_Etiquetas_Etiqueta_Id",
                table: "ArticuloEtiqueta",
                column: "Etiqueta_Id",
                principalTable: "Etiquetas",
                principalColumn: "Etiqueta_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloEtiqueta_Tbl_Articulo_Articulo_Id",
                table: "ArticuloEtiqueta",
                column: "Articulo_Id",
                principalTable: "Tbl_Articulo",
                principalColumn: "Articulo_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
