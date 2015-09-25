using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class JerarquiaController : Controller
    {
        // GET: Jerarquia
        public ActionResult AddJefe()
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                var query = client.getTbl_usuarios();
                List<string> emple = new List<string>();
                foreach (var item in query)
	            {
		            emple.Add(item.primer_nombre + " " + item.primer_apellido + "|" + item.talento_humano);
	            }
                ViewBag.Empleados = emple;

                var quer = client.getTbl_departamentos();
                List<string> depto = new List<string>();
                foreach (var item in quer)
                {
                    depto.Add(item.descripcion+ "|" + item.departamentoid);
                }
                client.Close();
                ViewBag.Empleados = emple;
                ViewBag.Departamentos = depto;


                return View();
            }
                
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AddJefe(JerarquiaModel model)
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                int deptoId = int.Parse(splitID(model.departamentoid.ToString()));
                int talentoH = int.Parse(splitID(model.talento_humano_jefe.ToString()));
                if(deptoId!=0&&talentoH!=00)
                {
                    client.deleteDepartamento_Jefe(deptoId);
                    client.addDepartamento_Jefe(talentoH, deptoId);
                }

                var query = client.getTbl_usuarios();
                var quer = client.getTbl_departamentos();
                client.Close();
                List<string> emple = new List<string>();
                foreach (var item in query)
                {
                    emple.Add(item.primer_nombre + " " + item.primer_apellido + "|" + item.talento_humano);
                }
                ViewBag.Empleados = emple;

                List<string> depto = new List<string>();
                foreach (var item in quer)
                {
                    depto.Add(item.descripcion + "|" + item.departamentoid);
                }
                ViewBag.Empleados = emple;
                ViewBag.Departamentos = depto;
                return View();
            }

            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult getJefe_Departamanto()
        {

            string json = "";
            Service1Client client = new Service1Client();
            var depto = client.getTbl_departamentos();
            for (int i = 0; i < depto.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                int deptoid = depto.ElementAt(i).departamentoid;
                string jefe = client.getJefe_Departamento(deptoid);
                json += "{" + String.Format("\"departamento\":\"{0}\",\"nombre\":\"{1}\"", depto.ElementAt(i).descripcion, jefe) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

       // [HttpPost]
        [HttpPost]
        public ActionResult getUsuarios_TalentoHumano()
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getTbl_usuarios();
            if (query == null)
            {
                json += String.Format("\"{0}\"", "");
                json = "[" + json + "]";
                return Content(json);
            }
            for (int i = 0; i < query.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                string Usuario = query.ElementAt(i).primer_nombre + " " + query.ElementAt(i).primer_apellido + "|" + query.ElementAt(i).talento_humano;
                json += String.Format("\"{0}\"", Usuario);

           }

           json = "[" + json + "]";
            return Content(json);
        }

        private string splitID(string cadena)
        {
            string valor = "0";
            bool id = false;
            if (cadena == null)
                return valor;
            for (int i = 0; i < cadena.Length; i++)
            {
                if (cadena[i] == '|')
                {
                    id = true;
                    valor = "";
                }
                else if(id)
                {
                    valor += cadena[i];
                }

            }
            return valor;
        }

        public ActionResult EditarJefeEmpleado()
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                var query = client.getTbl_usuarios();
                List<string> emple = new List<string>();
                foreach (var item in query)
                {
                    emple.Add(item.primer_nombre + " " + item.primer_apellido + "|" + item.talento_humano);
                }
                ViewBag.Emple = emple;

                return View();
            }

            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult EditarJefeEmpleado(JerarquiaModel model, string submitButton)
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                if (submitButton.Equals("Editar"))
                {
                    var query = client.getTbl_usuarios();
                    var hola = client.getUsuario(model.talento_humano);
                    UsuariosModel usuario = new UsuariosModel();
                    List<string> emple = new List<string>();
                    foreach (var item in query)
                    {
                        emple.Add(item.primer_nombre + " " + item.primer_apellido + "|" + item.talento_humano);
                    }
                    ViewBag.Emple = emple;
                    if (hola == null)
                    {
                        client.Close();
                        return View();
                    }
                    ViewBag.nombre = hola.primer_nombre;
                    ViewBag.apellido = hola.segundo_apellido;
                    ViewBag.nombre = ViewBag.primerombre + ViewBag.apellido;
                }

                client.Close();
                return View();

            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult getDptJefe()
        {

            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getDepartamento_Usuario();
            for (int i = 0; i < query.ElementAt(0).Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                int thEm=int.Parse(query.ElementAt(3).ElementAt(i));
                int depto = int.Parse(query.ElementAt(1).ElementAt(i));
                int talentoJefe = client.getTalentoHumano_Jefe(thEm,depto);
                var usu = client.getUsuario(talentoJefe);
                 string NombreJefe="";
                 if (usu == null)
                 {
                     NombreJefe = "Sin Asignar";
                     talentoJefe = 0;
                 }
                 else
                 {
                     NombreJefe = usu.primer_nombre + " " + usu.primer_apellido;
                 }
               // string jefe = client.getj
                json += "{" + String.Format("\"descripcion\":\"{0}\",\"departamentoid\":\"{1}\",\"nombre\":\"{2}\",\"talento_humano\":\"{3}\",\"nombreJefe\":\"{4}\",\"talentoJefe\":\"{5}\"", query.ElementAt(0).ElementAt(i), query.ElementAt(1).ElementAt(i), query.ElementAt(2).ElementAt(i), query.ElementAt(3).ElementAt(i),NombreJefe,talentoJefe) + "}";


            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }
    }
}