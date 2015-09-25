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
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult getJefe_Departamanto()
        {

            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getDepartamentoJefe();
            for (int i = 0; i < query.ElementAt(0).Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"departamento\":\"{0}\",\"nombre\":\"{1}\"", query.ElementAt(0).ElementAt(i), query.ElementAt(1).ElementAt(i)) + "}";


            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

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
    }
}