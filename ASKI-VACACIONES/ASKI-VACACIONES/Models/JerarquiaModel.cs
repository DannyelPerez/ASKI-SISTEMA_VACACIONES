using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASKI_VACACIONES.Models
{
    public class JerarquiaModel
    {
        public UsuariosModel usuarios { get; set; }
        public int talento_humano { get; set; }
        public int talento_humano_jefe { get; set; }
        public int departamentoid { get; set; }
    }
}