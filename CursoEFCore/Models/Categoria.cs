using System.ComponentModel.DataAnnotations;

namespace CursoEFCore.Models
{
    public class Categoria
    {
        public int Categoria_Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }


        // Relacion 1 - Muchos
        // Esta es Tabla Padre y Articulo Tabla Hila
        public List<Articulo> Articulo { get; set; }
    }
}
