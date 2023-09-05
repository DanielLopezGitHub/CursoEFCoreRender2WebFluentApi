﻿// <auto-generated />
using System;
using CursoEFCore.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CursoEFCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230905221401_SiembraDeDatosCategoria2Registros")]
    partial class SiembraDeDatosCategoria2Registros
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CursoEFCore.Models.Articulo", b =>
                {
                    b.Property<int>("Articulo_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Articulo_Id"), 1L, 1);

                    b.Property<double>("Calificacion")
                        .HasColumnType("float");

                    b.Property<int?>("Categoria_Id")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Tiempo")
                        .HasColumnType("datetime2");

                    b.Property<string>("TituloArticulo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Titulo");

                    b.HasKey("Articulo_Id");

                    b.HasIndex("Categoria_Id");

                    b.ToTable("Tbl_Articulo");
                });

            modelBuilder.Entity("CursoEFCore.Models.ArticuloEtiqueta", b =>
                {
                    b.Property<int>("Articulo_Id")
                        .HasColumnType("int");

                    b.Property<int>("Etiqueta_Id")
                        .HasColumnType("int");

                    b.HasKey("Articulo_Id", "Etiqueta_Id");

                    b.HasIndex("Etiqueta_Id");

                    b.ToTable("ArticuloEtiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Categoria", b =>
                {
                    b.Property<int>("Categoria_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Categoria_Id"), 1L, 1);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Categoria_Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Categoria_Id = 77,
                            Activo = true,
                            FechaCreacion = new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Categoria 5"
                        },
                        new
                        {
                            Categoria_Id = 78,
                            Activo = false,
                            FechaCreacion = new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Categoria 6"
                        });
                });

            modelBuilder.Entity("CursoEFCore.Models.DetalleUsuario", b =>
                {
                    b.Property<int>("DetalleUsuario_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetalleUsuario_Id"), 1L, 1);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Deporte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mascota")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetalleUsuario_Id");

                    b.ToTable("DetalleUsuarios");
                });

            modelBuilder.Entity("CursoEFCore.Models.Etiqueta", b =>
                {
                    b.Property<int>("Etiqueta_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Etiqueta_Id"), 1L, 1);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Etiqueta_Id");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("CursoEFCore.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DetalleUsuario_Id")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DetalleUsuario_Id")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("CursoEFCore.Models.Articulo", b =>
                {
                    b.HasOne("CursoEFCore.Models.Categoria", "Categoria")
                        .WithMany("Articulo")
                        .HasForeignKey("Categoria_Id");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("CursoEFCore.Models.ArticuloEtiqueta", b =>
                {
                    b.HasOne("CursoEFCore.Models.Articulo", "Articulo")
                        .WithMany("ArticuloEtiqueta")
                        .HasForeignKey("Articulo_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CursoEFCore.Models.Etiqueta", "Etiqueta")
                        .WithMany("ArticuloEtiqueta")
                        .HasForeignKey("Etiqueta_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("Etiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Usuario", b =>
                {
                    b.HasOne("CursoEFCore.Models.DetalleUsuario", "DetalleUsuario")
                        .WithOne("Usuario")
                        .HasForeignKey("CursoEFCore.Models.Usuario", "DetalleUsuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetalleUsuario");
                });

            modelBuilder.Entity("CursoEFCore.Models.Articulo", b =>
                {
                    b.Navigation("ArticuloEtiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Categoria", b =>
                {
                    b.Navigation("Articulo");
                });

            modelBuilder.Entity("CursoEFCore.Models.DetalleUsuario", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CursoEFCore.Models.Etiqueta", b =>
                {
                    b.Navigation("ArticuloEtiqueta");
                });
#pragma warning restore 612, 618
        }
    }
}
