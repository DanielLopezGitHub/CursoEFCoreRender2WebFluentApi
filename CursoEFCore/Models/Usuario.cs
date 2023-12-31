﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        [EmailAddress(ErrorMessage = "Email Invalido")]
        public string Email { get; set; }

        [Display(Name = "Direccion del Usuario")]
        public string Direccion { get; set; }

        public int Edad { get; set; }

        // Relacion 1 - 1:
        // Esta es la Tabla Padre y DetalleUsuario la tabla Hija
        public int? DetalleUsuario_Id { get; set; }
        public DetalleUsuario DetalleUsuario { get; set; }
    }
}
