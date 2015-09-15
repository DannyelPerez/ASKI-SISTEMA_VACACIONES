using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.EntityClient;
using Service_Asky.Tables;
using MySql.Data.MySqlClient;

namespace Service_Asky
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //cambiar dependiendo del servidor 
        DBConnect connect = new DBConnect("localhost", "root", "1234");
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


        //=================== Add Element to database=============
        public void addDepartamento(string descripcion)
        {
            try
            {
                vsystem_askiEntities db = new vsystem_askiEntities();
                tbl_departamento dep = new tbl_departamento();
                dep.descripcion = descripcion;
                dep.activo = true;
                db.tbl_departamento.Add(dep);
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }

        }
        public void addRole(string descripcion)
        {
            try
            {
                vsystem_askiEntities db = new vsystem_askiEntities();
                tbl_roles roles = new tbl_roles();
                roles.descripcion = descripcion;
                roles.activo = true;
                db.tbl_roles.Add(roles);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void addUsuario(int talento_humano, string email, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, DateTime fecha_ingreso, string password)
        {
            try
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
            catch (Exception ex)
            {

            }
        }

        public void addPermiso(string descripcion)
        {
            try
            { 
            vsystem_askiEntities db = new vsystem_askiEntities();
            tbl_permisos permisos = new tbl_permisos();
            permisos.descripcion = descripcion;
            permisos.activo = true;
            db.tbl_permisos.Add(permisos);
            db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }


        public  void addUsuario_Departamento(int talentoHumano, int idDepartamento)
        {
            try
            {

                string query = "INSERT INTO tbl_usuarios_departamento (talento_humano, departamentoid) VALUES('" + talentoHumano + "', '" + idDepartamento + "')";
                if (connect.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connect.getConnection());
                    cmd.ExecuteNonQuery();
                    connect.CloseConnection();
                }
            }
            catch(Exception ex)
            {

            }
        }
        
        public void addUsuario_Rol(int talentoHumano, int idRol)
        {
            try
            {

                string query = "INSERT INTO tbl_usuarios_roles (talento_humano, rolesid) VALUES('" + talentoHumano + "', '" + idRol + "')";
                if (connect.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connect.getConnection());
                    cmd.ExecuteNonQuery();
                    connect.CloseConnection();
                }

            }
            catch (Exception ex)
            {

            }
        }
        
        public void addRoles_Permisos(int idRol, int idPermiso)
        {
            try
            {
            string query = "INSERT INTO tbl_roles_permisos (rolesid, permisosid) VALUES('" + idRol + "', '" + idPermiso + "')";
            if (connect.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connect.getConnection());
                cmd.ExecuteNonQuery();
                connect.CloseConnection();
            }
            }
            catch (Exception ex)
            {

            }
        }


        //=================== Edit Element from database=============

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
        public void editUsuario(int talentoHumano, string email, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, DateTime fechaIngreso)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var dic = (from p in db.tbl_usuarios
                       where p.talento_humano == talentoHumano
                       select p)
                       .FirstOrDefault();

            if (dic != null)
            {
                dic.talento_humano = talentoHumano;
                dic.email = email;
                dic.primer_nombre = primerNombre;
                dic.segundo_nombre = segundoNombre;
                dic.primer_apellido = primerApellido;
                dic.segundo_apellido = segundoApellido;
                dic.fecha_ingreso = fechaIngreso;
                db.SaveChanges();
            }

        }



        //=================== Delete Element from database=============
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




        //=================== Get Elements from database=============

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
        public Permisos getPermiso(int id)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            var perm = (from p in db.tbl_permisos where p.permisosid == id select p).FirstOrDefault();
            if (perm == null)
                return null;
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
            if (dic == null)
                return null;
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
            if (dic == null)
                return null;
            Departamento dep = new Departamento();
            dep.departamentoid = dic.departamentoid;
            dep.descripcion = dic.descripcion;
            dep.activo = dic.activo;
            return dep;

        }
        public Usuario getUsuario(int talento_humano)
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            Usuario u = new Usuario();
            var user = db.tbl_usuarios.Where(x => x.talento_humano.Equals(talento_humano)).FirstOrDefault();
            if (user == null)
                return null;
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

         public List<Usuario> getTbl_usuarios()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Usuario> usuario = new List<Usuario>();
            foreach (var user in db.tbl_usuarios)
            {
                Usuario u = new Usuario();
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
                usuario.Add(u);
            }
            return usuario;
        }
        public List<Calendario> getTbl_calendario()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Calendario> calendario = new List<Calendario>();
            foreach (var item in db.tbl_calendario)
            {
                Calendario c = new Calendario();
                c.fecha = item.fecha;
                c.talento_humano_empleado = item.talento_humano_empleado;
                c.talento_humano_jefe = item.talento_humano_jefe;
                c.tipo_dia_id = item.tipo_dia_id;
                calendario.Add(c);

            }
            return calendario;

        }
        public List<Departamento> getTbl_departamentos()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Departamento> departamentos = new List<Departamento>();
            foreach (var item in db.tbl_departamento)
            {
                Departamento d = new Departamento();
                d.activo = item.activo;
                d.departamentoid = item.departamentoid;
                d.descripcion = item.descripcion;
                departamentos.Add(d);
            }
            return departamentos;
        }
        public List<Jerarquia> getTbl_jerarquia()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Jerarquia> jerarquia = new List<Jerarquia>();
            foreach (var item in db.tbl_jerarquia)
            {
                Jerarquia j = new Jerarquia();
                j.departamentoid = item.departamentoid;
                j.jefe_talentohumano = item.jefe_talentohumano;
                j.jerarquiaid = item.jerarquiaid;
                j.talento_humano = item.talento_humano;
                jerarquia.Add(j);
            }
            return jerarquia;
        }
        public List<Log_Vacaciones> getTbl_log_vacaciones()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Log_Vacaciones> Log_Vacaciones = new List<Log_Vacaciones>();
            foreach (var item in db.tbl_log_vacaciones)
            {
                Log_Vacaciones l = new Log_Vacaciones();
                l.estatus_actual = item.estatus_actual;
                l.estatus_anterior = item.estatus_anterior;
                l.fecha_modificacion = item.fecha_modificacion;
                l.logid = item.logid;
                l.th_modifico = item.th_modifico;
                l.vacacionesid = item.vacacionesid;
                Log_Vacaciones.Add(l);
            }
            return Log_Vacaciones;
        }
        public List<Permisos> getTbl_permisos()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Permisos> permisos = new List<Permisos>();
            foreach (var item in db.tbl_permisos)
            {
                Permisos p = new Permisos();
                p.activo = item.activo;
                p.permisosid = item.permisosid;
                p.descripcion = item.descripcion;
                permisos.Add(p);
            }
            return permisos;
        }
        public List<Roles> getTbl_roles()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Roles> roles = new List<Roles>();
            foreach (var item in db.tbl_roles)
            {
                Roles r = new Roles();
                r.activo = item.activo;
                r.rolesid = item.rolesid;
                r.descripcion = item.descripcion;
                roles.Add(r);
            }
            return roles;
        }
        public List<Status> getTbl_status()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Status> status = new List<Status>();
            foreach (var item in db.tbl_estatus)
            {
                Status s = new Status();
                s.activo = item.activo;
                s.estatusid = item.estatusid;
                s.descripcion = item.descripcion;
                status.Add(s);
            }
            return status;
        }
        public List<Tipo_Dia> getTbl_tipo_dia()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Tipo_Dia> tipoDia = new List<Tipo_Dia>();
            foreach (var item in db.tbl_tipo_dia)
            {
                Tipo_Dia t = new Tipo_Dia();
                t.tipo_dia_id = item.tipo_dia_id;
                t.descripcion = item.descripcion;
                tipoDia.Add(t);
            }
            return tipoDia;
        }
        public List<Vacaciones> getTbl_vacaciones()
        {
            vsystem_askiEntities db = new vsystem_askiEntities();
            List<Vacaciones> vacaciones = new List<Vacaciones>();
            foreach (var item in db.tbl_vacaciones)
            {
                Vacaciones v = new Vacaciones();
                v.dias_solicitados = item.dias_solicitados;
                v.estatusid = item.estatusid;
                v.fecha_de_aprobacion = item.fecha_de_aprobacion;
                v.fecha_entrada = item.fecha_entrada;
                v.fecha_salida = item.fecha_salida;
                v.fecha_solicitud = item.fecha_solicitud;
                v.talento_humano = item.talento_humano;
                v.vacacionesid = item.vacacionesid;
                v.year = item.year;
                vacaciones.Add(v);
            }
            return vacaciones;
        }

        public int getUltimoId_Roles()
        {
            try
            {
                vsystem_askiEntities db = new vsystem_askiEntities();
                var id = db.tbl_roles.OrderByDescending(x => x.rolesid).Take(1);
                return int.Parse(id.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


 
    }
}
