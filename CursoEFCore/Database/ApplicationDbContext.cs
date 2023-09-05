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


        // Relacion Muchos - Muchos
        // Articulo - Etiqueta
        // Utilizando Api Fluente:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definiendole una llave Primaria (HasKey) a cada campo de la Tabla Intermedia ArticuloEtiqueta
            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new {ae.Articulo_Id, ae.Etiqueta_Id});


            // Data Seeding
            var categoria5 = new Categoria() { Categoria_Id = 77, Nombre = "Categoria 5", FechaCreacion = new DateTime(2023, 6, 15), Activo = true };
            var categoria6 = new Categoria() { Categoria_Id = 78, Nombre = "Categoria 6", FechaCreacion = new DateTime(2023, 6, 16), Activo = false };
            // pordriamos crear mas registros, 100 o mas aqui en seguida.
            modelBuilder.Entity<Categoria>().HasData(new Categoria[] { categoria5, categoria6 });
            base.OnModelCreating(modelBuilder);
            // Luego se hace una migracion y update database
            // Para eliminar uno de estos registros sembrados solo es quitar un objeto de categoria del array que se subira
            // y hacer otra migracion y actuaizacion, y se borrara de la DB.
        }
    }
}
