using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class EventosController : Controller
    {
        // GET: Ccalendario
        public ViewResult Eventos()
        {
            if (Session["User"] != null)
                return View();
            else
                return View("Login");
        }

        [HttpPost]
        public ActionResult Eventos(TipoDiaModel tipo)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    client.addTipo_dia(tipo.descripcion);
                    client.Close();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Eventos(EventosModel calendario)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                   client.addCalendario(int.Parse(Session["Talento_Humano"].ToString()),calendario.Fecha, calendario.Tipo_dia_id);
                    client.Close();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

    }
}