using System.ComponentModel.DataAnnotations;

namespace CursoEFCore.Models
{
    public class Categoria
    {
        [Key]
        public int Categoria_Id { get; set; }

        //[DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[NULL]")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }


        // Relacion 1 - Muchos
        // Esta es Tabla Padre y Articulo Tabla Hila
        public List<Articulo> Articulo { get; set; }
    }
}
