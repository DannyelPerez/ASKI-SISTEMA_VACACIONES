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
            return View();
        }

        public ActionResult EditarJefeEmpleado()
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                var query = client.getTbl_departamentos();
                List<string> depto = new List<string>();
                foreach (var item in query)
                {
                    depto.Add(item.departamentoid + " " + "|" + item.descripcion);
                }
                ViewBag.depto = depto;

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
                json += "{" + String.Format("\"descripcion\":\"{0}\",\"departamentoid\":\"{1}\",\"nombre\":\"{2}\",\"talento_humano\":\"{3}\"", query.ElementAt(0).ElementAt(i), query.ElementAt(1).ElementAt(i), query.ElementAt(2).ElementAt(i), query.ElementAt(3).ElementAt(i)) + "}";


            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }
    }
}