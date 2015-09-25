using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ASKI_VACACIONES.Models;

namespace ASKI_VACACIONES.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult JSon()
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
                json +=  String.Format("\"{0}\"", Usuario);


            }

           // json = "[" + json + "]";
            return Content(json);

        }
    }
}