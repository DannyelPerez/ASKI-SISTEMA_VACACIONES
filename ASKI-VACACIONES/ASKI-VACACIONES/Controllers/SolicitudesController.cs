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
                    List<int> idvacaciones  = splitCadenaID(solicitudes.idvacaciones);
                    int talento = int.Parse(Session["Talento_Humano"].ToString());
                   // Year = (int)((DateTime.Now - ));
                    if (idvacaciones == null)
                        return View();
                    client.addVacacion(talento,solicitudes.year,solicitudes.Fechainicio,solicitudes.Fechainicio,,solicitudes.fecha_solicitud,fechaAprobacion,)

                    foreach (var item in idvacaciones)
                    {
                        client.addRoles_Permisos(client.getUltimoId_Roles(), item);
                    }
                    client.Close();
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