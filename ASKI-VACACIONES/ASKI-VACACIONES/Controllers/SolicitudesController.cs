using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASKI_VACACIONES.ServiceReference1;
using ASKI_VACACIONES.Models;

namespace ASKI_VACACIONES.Controllers
{
    public class SolicitudesController : Controller
    {
        // GET: Solicitudes
        public ActionResult NuevaSolicitud() 
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                var query = client.getTbl_usuarios();
                client.Close();
                List<string> emple = new List<string>();
                foreach (var item in query)
                {
                    emple.Add(item.primer_nombre + " " + item.primer_apellido + "|" + item.talento_humano);
                }
                ViewBag.jefe = emple;

                
                return View();
            }

            else
                return RedirectToAction("Login", "Home");
        }
    [HttpPost]
        public ActionResult NuevaSolicitud(SolicitudesModel solicitudes)
        {

            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                if (ModelState.IsValid)
                {
                    
                    int talento = int.Parse(Session["Talento_Humano"].ToString()); 
       


                    if (client.approveRequest(solicitudes.Fechainicio, solicitudes.Fechafin) == true)
                    {
                        client.addVacacion(talento,solicitudes.Fechainicio, solicitudes.Fechafin, DateTime.Now.Date, DateTime.Now.Date, 1);
                    
                    }
                }
                var query = client.getTbl_usuarios();
                client.Close();
                List<string> emple = new List<string>();
                foreach (var item in query)
                {
                    emple.Add(item.primer_nombre + " " + item.primer_apellido + "|" + item.talento_humano);
                }
                ViewBag.jefe = emple;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        public ActionResult MisSolicitudes() 
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        private List<int> splitCadenaID(string cadenaID)
        {
            List<int> numero = new List<int>();
            string valor = "";
            for (int i = 0; i < cadenaID.Length; i++)
            {
                if (cadenaID[i] == '/')
                {
                    numero.Add(int.Parse(valor));
                    valor = "";
                }
                else
                {
                    valor += cadenaID[i];
                }

            }
            return numero;
        }

        public ActionResult Aprobar_solicitud() { return View(); }
        [HttpPost]
        public ActionResult getsolicitud()
        {

            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getsolicitud_pendiente(int.Parse(Session["Talento_humano"].ToString()));
            for (int i = 0; i < query.ElementAt(0).Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                
                // string jefe = client.getj
                json += "{" + String.Format("\"Nombre\":\"{0}\",\"fecha_salida\":\"{1}\",\"fecha_entrada\":\"{2}\",\"dias\":\"{3}\"", query.ElementAt(0).ElementAt(i), query.ElementAt(1).ElementAt(i), query.ElementAt(2).ElementAt(i), query.ElementAt(3).ElementAt(i)) + "}";


            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }
    }
}