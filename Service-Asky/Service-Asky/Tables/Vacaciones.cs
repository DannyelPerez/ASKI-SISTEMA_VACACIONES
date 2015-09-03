using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service_Asky.Tables
{
    public class Vacaciones
    {
        public int vacacionesid { get; set; }
        public int talento_humano { get; set; }
        public int year { get; set; }
        public System.DateTime fecha_salida { get; set; }
        public System.DateTime fecha_entrada { get; set; }
        public int dias_solicitados { get; set; }
        public int fecha_solicitud { get; set; }
        public System.DateTime fecha_de_aprobacion { get; set; }
        public int estatusid { get; set; }
    }
}