using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Service_Asky.Tables
{
    [DataContract(IsReference=true)]
    public class Usuario
    {
        [DataMember]
        public int talento_humano { get; set; }
                 [DataMember]
        public string email { get; set; }
                 [DataMember]
        public string primer_nombre { get; set; }
         [DataMember]
        public string segundo_nombre { get; set; }
         [DataMember]
        public string primer_apellido { get; set; }
         [DataMember]
        public string segundo_apellido { get; set; }
         [DataMember]
        public System.DateTime fecha_ingreso { get; set; }
         [DataMember]
        public System.DateTime fecha_creacion { get; set; }
         [DataMember]
        public string password { get; set; }
         [DataMember]
        public bool activo { get; set; }

         [DataMember]
         public virtual Usuario Usuarios { get; set; }
    
    }
}