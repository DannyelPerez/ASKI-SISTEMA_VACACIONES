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
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    int talento = int.Parse(Session["Talento_Humano"].ToString());

                    if (client.approveRequest(solicitudes.Fechainicio, solicitudes.Fechafin) == true)
                    {
                        client.addVacacion(talento,,solicitudes.Fechainicio,solicitudes.Fechafin,DateTime.Now.Date,null,1);
                    }
                }
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
    }
}