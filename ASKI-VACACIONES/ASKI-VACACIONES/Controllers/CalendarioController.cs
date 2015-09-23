using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class CalendarioController : Controller
    {
        // GET: Ccalendario
        public ViewResult Calendario()
        {
            if (Session["User"] != null)
                return View();
            else
                return View("Login");
        }

        [HttpPost]
        public ActionResult Calendario(TipoDiaModel calendario)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    client.addTipo_dia(calendario.descripcion, calendario.color);
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