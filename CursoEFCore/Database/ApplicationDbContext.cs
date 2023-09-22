using CursoEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Database
{
    public class ApplicationDbContext : DbContext
    {
        // Recibiendo la instancia "opciones" desde el Program.cs con Inyeccion de Dependencias y
        // pasando "opciones" al Ctor de la clase base "DbContext".
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones){}



        // Modelos-Tablas manjeados en la Database

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<DetalleUsuario> DetalleUsuarios { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<ArticuloEtiqueta> ArticuloEtiquetas { get; set; }


        // Relacion Muchos - Muchos
        // Articulo - Etiqueta
        // Utilizando Api Fluente:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // - - --  - - - - - - - --  - Fluent Api Part - --  - -  - - - --  -- - -- - --- -



            // -- Para Categoria --
            
            // Indicando PK para Categoria
            modelBuilder.Entity<Categoria>().HasKey(c => c.Categoria_Id);
            // Indicando campo requerido para nombre
            modelBuilder.Entity<Categoria>().Property(c => c.Nombre).IsRequired();
            // Indicando campo que se trabajara como una fecha
            modelBuilder.Entity<Categoria>().Property(c => c.FechaCreacion).HasColumnType("date");


            // -- Para Articulo --

            // Indicando PK para Articulo
            modelBuilder.Entity<Articulo>().HasKey(a => a.Articulo_Id);
            // Inidicando que la propiedad Titulo es requerido y el largo maximo de string es de 20
            modelBuilder.Entity<Articulo>().Property(a => a.TituloArticulo).IsRequired().HasMaxLength(20);
            // Inidicando que la propiedad Descripcion es requerido y el Largo Maximo de string es de 500
            modelBuilder.Entity<Articulo>().Property(a => a.Descripcion).IsRequired().HasMaxLength(500);
            // Indicando que el Tipo de dato para Fecha es Date.
            modelBuilder.Entity<Articulo>().Property(a => a.Fecha).HasColumnType("date");
            // Indicando Nombre de Tabla
            modelBuilder.Entity<Articulo>().ToTable("Tbl_Articulo");
            // Indicando de Nombre de Columna
            modelBuilder.Entity<Articulo>().Property(a => a.TituloArticulo).HasColumnName("Titulo");


            // -- Para Usuario --

            // Indicando PK para Usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            // Indicando que NO se mapee la propiedad Edad en la DB
            modelBuilder.Entity<Usuario>().Ignore(u => u.Edad);


            // -- Para DetalleUsuario --

            // Indicando PK para DetalleUsuario
            modelBuilder.Entity<DetalleUsuario>().HasKey(du => du.DetalleUsuario_Id);
            // Indicando que el Campo Cedula es Requerido
            modelBuilder.Entity<DetalleUsuario>().Property(du => du.Cedula).IsRequired();


            // -- Para Etiqueta --

            // Indicando PK para Etiqueta
            modelBuilder.Entity<Etiqueta>().HasKey(e => e.Etiqueta_Id);
            // Indicando que es un campo tipo Fecha
            modelBuilder.Entity<Etiqueta>().Property(e => e.Fecha).HasColumnType("date");



            //  - - - - - RELACIONES - - - - -


            // -- Relacion 1 - 1 entre Usuario y DetalleUsuario --
            modelBuilder.Entity<Usuario>().HasOne(u => u.DetalleUsuario).WithOne(u => u.Usuario).HasForeignKey<Usuario>("DetalleUsuario_Id");

            // -- Relacion 1 - Muchos entre Categoria y Articulo --
            modelBuilder.Entity<Articulo>().HasOne(a => a.Categoria).WithMany(a => a.Articulo).HasForeignKey(a => a.Categoria_Id);

            // -- Relacion Muchos - Muchos entre Articulo y Etiqueta --
            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new { ae.Etiqueta_Id, ae.Articulo_Id });
            modelBuilder.Entity<ArticuloEtiqueta>().HasOne(a => a.Articulo).WithMany(a => a.ArticuloEtiqueta).HasForeignKey(c => c.Articulo_Id);
            modelBuilder.Entity<ArticuloEtiqueta>().HasOne(a => a.Etiqueta).WithMany(a => a.ArticuloEtiqueta).HasForeignKey(c => c.Etiqueta_Id);
            
            
            
            // - - --  - - - - - - - --  - Fluent Api Part - --  - -  - - - --  -- - -- - --- -

            base.OnModelCreating(modelBuilder);
        }
    }
}
