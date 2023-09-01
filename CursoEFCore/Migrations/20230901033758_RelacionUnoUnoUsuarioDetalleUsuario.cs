using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEFCore.Migrations
{
    public partial class RelacionUnoUnoUsuarioDetalleUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetalleUsuario_Id",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DetalleUsuario_Id",
                table: "Usuarios",
                column: "DetalleUsuario_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_DetalleUsuarios_DetalleUsuario_Id",
                table: "Usuarios",
                column: "DetalleUsuario_Id",
                principalTable: "DetalleUsuarios",
                principalColumn: "DetalleUsuario_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_DetalleUsuarios_DetalleUsuario_Id",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_DetalleUsuario_Id",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DetalleUsuario_Id",
                table: "Usuarios");
        }
    }
}
