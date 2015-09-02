using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ASKI_VACACIONES.Models
{
    public class UsuariosModel
    {
        [Required(ErrorMessage = "Porfavor escriba el talento humano")]
        public int talento_humano { get; set; }

        [Required(ErrorMessage = "Porfavor escriba el correo")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Porfavor escriba su primer nombre")]
        public string primer_nombre { get; set; }

        [Required(ErrorMessage = "Porfavor escriba su segundo nombre")]
        public string segundo_nombre { get; set; }

        [Required(ErrorMessage = "Porfavor escriba su primer apellido")]
        public string primer_apellido { get; set; }

        [Required(ErrorMessage = "Porfavor escriba su segundo apellido")]
        public string segundo_apellido { get; set; }

        [Required(ErrorMessage = "Porfavor escriba su fecha de ingreso")]
        public DateTime fecha_ingreso { get; set; }

        public DateTime fecha_creacion { get; set; }

        public string password { get; set; }

        public bool activo { get; set; }
    }

    public class vsystem_askiDBContext:DbContext
    {
        //Test
        public DbSet<UsuariosModel> Usuarios { get; set; }
    }
}