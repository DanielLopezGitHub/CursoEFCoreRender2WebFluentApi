using System.ComponentModel.DataAnnotations;

namespace CursoEFCore.Models
{
    public class Etiqueta
    {
        public int Etiqueta_Id { get; set; }

        public string Titulo { get; set; }

        public DateTime Fecha { get; set; }


        // Relacion Muchos - Muchos
        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; }
    }
}
