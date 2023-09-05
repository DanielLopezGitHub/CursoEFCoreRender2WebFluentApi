using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEFCore.Migrations
{
    public partial class SiembraDeDatosCategoria2Registros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Categoria_Id", "Activo", "FechaCreacion", "Nombre" },
                values: new object[] { 77, true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Categoria 5" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Categoria_Id", "Activo", "FechaCreacion", "Nombre" },
                values: new object[] { 78, false, new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Categoria 6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Categoria_Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Categoria_Id",
                keyValue: 78);
        }
    }
}
