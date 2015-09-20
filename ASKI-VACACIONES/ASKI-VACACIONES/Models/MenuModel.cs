using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASKI_VACACIONES.Models
{
    public class MenuModel
    {
        public bool Usuarios {get;set;}
        public bool Administracion { get; set; }
        public bool Departamentos {get;set;}
        public bool Roles {get;set;}
        public bool Calendario {get;set;}
        public bool Permisos {get;set;}
        public bool Reporte {get;set;}
        public bool Solicitud { get; set; }
    }
}