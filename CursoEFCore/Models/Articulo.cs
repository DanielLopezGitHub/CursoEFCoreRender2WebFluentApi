using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Models
{
    public class Articulo
    {
        // El Mapper de EF detecta que ArticuloId es una PK por esa sintaxis, si fuera IdArticulo ahi ya no funcionaria y no marcaria
        // la prop IdArticulo como PK, es una convencion.
        // public int ArticuloId { get; set; }

        public int Articulo_Id { get; set; }

        public string TituloArticulo { get; set; }

        public string Descripcion { get; set; }

        [Range(0.1, 5.0)] // Para establecer un rango valido que puede recibir este prop
        public double Calificacion { get; set; }
        
        public DateTime Fecha { get; set; }
        
        [DataType(DataType.Time)] // Para indicar que este campo es Tipo Solo Tiempo y no fecha
        public DateTime Tiempo { get; set; }

        // Para validar strings con Expresiones Regulares
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Email Invalido")]
        public string Email { get; set; }

        // Un Data Anotation especializado en validar Datos tipo Email
        [EmailAddress(ErrorMessage ="Email Invalido")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText ="[NULL]")] // Para convertir valores vacios del string a null
        public string Email2 { get; set; }

        [NotMapped] // Para NO mapear este prop en una tabla de la DB
        public int Edad { get; set; }



        // DATABASEGENERATED - distintas opciones para generar el IDPK de la Tabla

        // Este ID no lo va agenerar ni EF ni la DB ni nada, nosotros lo tenemos que enviar, por ejemplo la cedula medica podria
        // utilizarse de Id en este caso.
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        // public int IdNone { get; set; }

        // Este ID lo va a generar la DB pero no sera 1,1 autoincremental ni se podra actualizar, es un GUID por ejemplo, esto 
        // difiere en cada DB
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int IdIdentity { get; set; }

        // La DB generara un ID que con cada actualizacion del registro este Id ira cambiando, puede ser la fecha por egemplo.
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // public int IdComputed { get; set; }



        // Relacion 1 - Muchos
        // Esta es la tabla Hija y Categoria Tabla Padre, Creando FK para Categoria:
        // Esto simplemente ya añade una nueva columna a la tabla Articulo la cual sera la FK CategoriaId
        public Categoria Categoria { get; set; }

        // Esto ya es para agregar el FK de manera explicita colocandole un nombre personalizado.
        //[Display(Name = "Categoria")]
        public int? Categoria_Id { get; set; }



        // Relacion Muchos - Muchos
        public ICollection<ArticuloEtiqueta> ArticuloEtiqueta { get; set; }
    }
}
