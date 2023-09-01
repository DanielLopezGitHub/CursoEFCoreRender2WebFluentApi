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
    }
}
