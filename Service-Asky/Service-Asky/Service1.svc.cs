using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.EntityClient;
using Service_Asky.Tables;

namespace Service_Asky
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public void addDepartamento(string descripcion)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            tbl_departamento dep = new tbl_departamento();
            dep.descripcion = descripcion;
            dep.activo = true;
            db.tbl_departamento.Add(dep);
            db.SaveChanges();

        }
        public void addRole(string descripcion)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            tbl_roles roles = new tbl_roles();
            roles.descripcion = descripcion;
            roles.activo = true;
            db.tbl_roles.Add(roles);
            db.SaveChanges();

        }

        public void addUsuario(int talento_humano, string email, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, DateTime fecha_ingreso, string password)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            tbl_usuarios usuario = new tbl_usuarios();
            usuario.talento_humano = talento_humano;
            usuario.email = email;
            usuario.primer_nombre = primer_nombre;
            usuario.segundo_nombre = segundo_nombre;
            usuario.primer_apellido = primer_apellido;
            usuario.segundo_apellido = segundo_apellido;
            usuario.fecha_ingreso = fecha_ingreso;
            usuario.fecha_creacion = DateTime.Today;
            usuario.password = password;
            usuario.activo = true;
            db.tbl_usuarios.Add(usuario);
            db.SaveChanges();
        }

        public void addPermiso(string descripcion)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            tbl_permisos permisos = new tbl_permisos();
            permisos.descripcion = descripcion;
            permisos.activo = true;
            db.tbl_permisos.Add(permisos);
            db.SaveChanges();

        }
      
        public void deletePermiso(int id)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var dic = (from p in db.tbl_permisos
                       where p.permisosid == id
                       select p)
                       .FirstOrDefault();

            if (dic != null)
            {
                dic.activo = false;
                db.SaveChanges();
            }
        }
        public void editPermiso(int id, string descripcion, bool Test)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            
            var dic = (from p in db.tbl_permisos
                       where p.permisosid == id
                       select p)
                       .FirstOrDefault();

            if (dic != null)
            {
                dic.descripcion = descripcion;
                db.SaveChanges();
            }
        }

        public void editRol(int id, string descripcion)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var dic = (from p in db.tbl_roles
                       where p.rolesid == id
                       select p)
                       .FirstOrDefault();

            if (dic != null)
            {
                dic.descripcion = descripcion;
                db.SaveChanges();
            }
        }

        public void editDepartamento(int id, string descripcion)
        {



            vsystem_askiEntities db = new vsystem_askiEntities();
            var dic = (from p in db.tbl_departamento
                       where p.departamentoid == id
                       select p)
                       .FirstOrDefault();

            if (dic != null)
            {
                dic.descripcion = descripcion;
                db.SaveChanges();
            }

        }

        public Permisos getPermiso(int id)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var perm = (from p in db.tbl_permisos where p.permisosid == id select p).FirstOrDefault();
            Permisos per = new Permisos();
            per.permisosid = perm.permisosid;
            per.descripcion = perm.descripcion;
            per.activo = perm.activo;
            return per;
        }
        public Roles getRol(int id)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var dic = (from p in db.tbl_roles where p.rolesid == id select p).FirstOrDefault();
            Roles rol = new Roles();
            rol.rolesid = dic.rolesid;
            rol.descripcion = dic.descripcion;
            rol.activo = dic.activo;
            return rol;
            
        }

        public Departamento getDepartamento(int id)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var dic = (from p in db.tbl_departamento
                       where p.departamentoid == id
                       select p)
                       .FirstOrDefault();
            Departamento dep = new Departamento();
            dep.departamentoid = dic.departamentoid;
            dep.descripcion = dic.descripcion;
            dep.activo = dic.activo;
            return dep;

        }

        public bool confirmarLogin(string email, string password)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var v = db.tbl_usuarios.Where(x => x.email.Equals(email) && x.password.Equals(password)).FirstOrDefault();
            if (v != null)
            {
                return true;

            }
            return false;
        }

        public Usuario getUsuario(int talento_humano)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            Usuario u = new Usuario();
            var user = db.tbl_usuarios.Where(x => x.talento_humano.Equals(talento_humano)).FirstOrDefault();
            u.talento_humano = user.talento_humano;
            u.email = user.email;
            u.primer_nombre = user.primer_nombre;
            u.segundo_nombre = user.segundo_nombre;
            u.primer_apellido = user.primer_apellido;
            u.segundo_apellido = user.segundo_apellido;
            u.fecha_ingreso = user.fecha_ingreso;
            u.fecha_creacion = user.fecha_creacion;
            u.password = user.password;
            u.activo = user.activo;
            return u;
        }
    }
}
