using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ASKI_VACACIONES.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Porfavor ingrese su correo electronico", AllowEmptyStrings = false)]
        public string email { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su contraseña", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string password { get; set; }
    }
}