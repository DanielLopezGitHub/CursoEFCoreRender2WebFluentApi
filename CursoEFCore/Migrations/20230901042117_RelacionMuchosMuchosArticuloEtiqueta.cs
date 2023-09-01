using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEFCore.Migrations
{
    public partial class RelacionMuchosMuchosArticuloEtiqueta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticuloEtiqueta",
                columns: table => new
                {
                    Articulo_Id = table.Column<int>(type: "int", nullable: false),
                    Etiqueta_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloEtiqueta", x => new { x.Articulo_Id, x.Etiqueta_Id });
                    table.ForeignKey(
                        name: "FK_ArticuloEtiqueta_Etiquetas_Etiqueta_Id",
                        column: x => x.Etiqueta_Id,
                        principalTable: "Etiquetas",
                        principalColumn: "Etiqueta_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticuloEtiqueta_Tbl_Articulo_Articulo_Id",
                        column: x => x.Articulo_Id,
                        principalTable: "Tbl_Articulo",
                        principalColumn: "Articulo_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloEtiqueta_Etiqueta_Id",
                table: "ArticuloEtiqueta",
                column: "Etiqueta_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloEtiqueta");
        }
    }
}
