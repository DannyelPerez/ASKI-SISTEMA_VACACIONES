using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASKI_VACACIONES.Models
{
    public class SolicitudesModel
    {
        public UsuariosModel usuarios { get; set; }
        public int talento_humano { get; set; }

        public int id { get; set; }
        public string idvacaciones { get; set; }
        public DateTime Fechainicio { get; set; }
        public DateTime Fechafin { get; set; }

        public DateTime year { get; set; }
        public int dias_solicitados { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public DateTime fecha_aprobacion { get; set; }
        public int estatudid { get; set; }

        


    }
}