using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service_Asky.Tables
{
    public class Log_Vacaciones
    {
        public int logid { get; set; }
        public int vacacionesid { get; set; }
        public int th_modifico { get; set; }
        public Nullable<int> estatus_anterior { get; set; }
        public int estatus_actual { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    }
}