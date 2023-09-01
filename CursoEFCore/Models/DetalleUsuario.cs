using System.ComponentModel.DataAnnotations;

namespace CursoEFCore.Models
{
    public class DetalleUsuario
    {
        [Key]
        public int DetalleUsuario_Id { get; set; }

        [Required]
        public string Cedula { get; set; }

        public string Deporte { get; set; }
        public string Mascota { get; set; }

        // Relacion 1 - 1:
        // Esta es la Tabla Hila y Usuario la tabla Padrea
        public Usuario Usuario { get; set; }
    }
}
