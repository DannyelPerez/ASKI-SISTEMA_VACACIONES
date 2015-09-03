using Service_Asky.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Service_Asky
{
    [DataContract]
    public partial class VSystem_AskiDataBase:DbContext
    {
        public VSystem_AskiDataBase()
    {
        Configuration.AutoDetectChangesEnabled = true;
        Configuration.LazyLoadingEnabled = true;
        Configuration.ProxyCreationEnabled = true;
        Configuration.ValidateOnSaveEnabled = true;
    }

        protected override void Dispose(bool disposing)
        {
            Configuration.LazyLoadingEnabled = false;
            base.Dispose(disposing);
        }

        public DbSet<Usuario> Usuarios { get; set; }

        //public List<Usuario> tbl_usuariosGet()
        //{
        //    List<Usuario> usuario = new List<Usuario>();
        //    foreach (var user in db.tbl_usuarios)
        //    {
        //        Usuario u = new Usuario();
        //        u.talento_humano = user.talento_humano;
        //        u.email = user.email;
        //        u.primer_nombre = user.primer_nombre;
        //        u.segundo_nombre = user.segundo_nombre;
        //        u.primer_apellido = user.primer_apellido;
        //        u.segundo_apellido = user.segundo_apellido;
        //        u.fecha_ingreso = user.fecha_ingreso;
        //        u.fecha_creacion = user.fecha_creacion;
        //        u.password = user.password;
        //        u.activo = user.activo;
        //        usuario.Add(u);
        //    }
            
        //    return usuario;
        //}

       
        //public Usuario getUsuario(int talento_humano)
        //{
        //    Usuario u = new Usuario();
        //    var user = db.tbl_usuarios.Where(x => x.talento_humano.Equals(talento_humano)).FirstOrDefault();
        //    u.talento_humano = user.talento_humano;
        //    u.email = user.email;
        //    u.primer_nombre = user.primer_nombre;
        //    u.segundo_nombre = user.segundo_nombre;
        //    u.primer_apellido = user.primer_apellido;
        //    u.segundo_apellido = user.segundo_apellido;
        //    u.fecha_ingreso = user.fecha_ingreso;
        //    u.fecha_creacion = user.fecha_creacion;
        //    u.password = user.password;
        //    u.activo = user.activo;
        //    return u;
        //}
    }
}