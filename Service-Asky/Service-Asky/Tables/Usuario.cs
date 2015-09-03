using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Service_Asky.Tables
{
    
    public class Usuario
    {
        
        public int talento_humano { get; set; }
      
        public string email { get; set; }
       
        public string primer_nombre { get; set; }
      
        public string segundo_nombre { get; set; }
   
        public string primer_apellido { get; set; }
 
        public string segundo_apellido { get; set; }
  
        public System.DateTime fecha_ingreso { get; set; }

        public System.DateTime fecha_creacion { get; set; }
  
        public string password { get; set; }
 
        public bool activo { get; set; }

    }
}