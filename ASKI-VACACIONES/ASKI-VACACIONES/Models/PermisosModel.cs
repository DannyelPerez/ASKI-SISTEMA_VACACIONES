using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASKI_VACACIONES.Models
{
    public class PermisosModel
    {
        public string descripcion { get; set; }
        [Required(ErrorMessage = "Escriba la descripcion")]
        public int id { get; set; }
        [Required(ErrorMessage = "Escriba el id")]
        public bool activo { get; set; }
    }
}