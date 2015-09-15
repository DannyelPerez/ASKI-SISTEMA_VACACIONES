using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASKI_VACACIONES.Models
{
    public class RolesModel
    {
        [Required(ErrorMessage = "Porfavor escriba la descripcion")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Porfavor escriba el id")]
        public int id { get; set; }

        public bool activo { get; set; }

        public string permisosID { get; set; }
    }
}