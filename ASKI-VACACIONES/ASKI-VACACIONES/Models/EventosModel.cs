using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASKI_VACACIONES.Models
{
    public class EventosModel
    {
        public int talento_humano_jefe { get; set; }
        public string Fecha { get; set; }
        public int Tipo_dia_id { get; set; }
       //tipo_dia
        public string descripcion { get; set; }
        //Permisos
        public string crear_eventos { get; set; }
        public string visualizar_eventos { get; set; }
    }
}