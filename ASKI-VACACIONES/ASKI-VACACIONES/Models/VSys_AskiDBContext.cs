using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.Entity;
using System.Data.Entity;
namespace ASKI_VACACIONES.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class VSys_AskiDBContext : DbContext
    {
        public VSys_AskiDBContext():base("name=VSys_AskiDBContext")
        {

        }
        public DbSet<UsuariosModel> Usuarios { get; set; }
    }
}