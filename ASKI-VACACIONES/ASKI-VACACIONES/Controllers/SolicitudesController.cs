using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASKI_VACACIONES.ServiceReference1;

namespace ASKI_VACACIONES.Controllers
{
    public class SolicitudesController : Controller
    {
        // GET: Solicitudes
        public ActionResult NuevaSolicitud() 
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult MisSolicitudes() 
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult getDptJefe()
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
    }
}