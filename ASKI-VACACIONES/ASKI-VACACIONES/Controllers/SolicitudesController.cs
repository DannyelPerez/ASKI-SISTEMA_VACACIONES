using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class SolicitudesController : Controller
    {
        // GET: Solicitudes
        public ActionResult NuevaSolicitud() {  return View();}
        public ActionResult MisSolicitudes() { return View(); }
    }
}