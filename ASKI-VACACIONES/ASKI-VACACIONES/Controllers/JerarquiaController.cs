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
    }
}