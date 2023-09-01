using System.ComponentModel.DataAnnotations;

namespace CursoEFCore.Models
{
    public class Categoria
    {
        [Key]
        public int Categoria_Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[NULL]")]
        public string Nombre { get; set; }
    }
}
