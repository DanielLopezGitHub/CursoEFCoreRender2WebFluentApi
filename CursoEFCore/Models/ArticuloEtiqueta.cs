using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Models
{
    public class ArticuloEtiqueta
    {
        public int Articulo_Id { get; set; }
        public int Etiqueta_Id { get; set; }

        public Articulo Articulo { get; set; }
        public Etiqueta Etiqueta { get; set; }


        // Relacion Muchos - Muchos
        // Tabla intermedia entre Articulo y Etiqueta
        // Cada uno de los IDs debe ser identificado por una PK pero no se puede añadir [KEY] dos veces.
        // Asi que esto se debe hacer con la Api Fluente, lo cual se debe escribir en el ApplicationDbContext.
    }
}
