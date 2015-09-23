using ASKI_VACACIONES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class MenuController : Controller
    {

       
        // GET: Menu2

        //[HttpGet]
        public ActionResult _Menu()
        {
           
            var permisos = Session["Permisos"] as string[];
            ViewBag.username = Session["User"];

            ViewBag.Usuarios = ViewBag.Administracion = ViewBag.Departamentos = ViewBag.Roles = ViewBag.Permisos = ViewBag.Reporte = ViewBag.Solicitud = ViewBag.Eventos = ViewBag.Jerarquia = false;
            foreach (var permiso in permisos)
            {
                switch (permiso)
                {

                    case "Administracion":
                        ViewBag.Administracion = true;
                        break;
                        
                    case "Usuarios":
                        ViewBag.Usuarios = true;
                        break;

                    case "Departamentos":
                        ViewBag.Departamentos = true;
                        break;

                    case "Roles":
                        ViewBag.Roles = true;
                        break;

                    case "Eventos":
                        ViewBag.Eventos = true;
                        break;

                    case "Permisos":
                        ViewBag.Permisos = true;
                        break;

                    case "Reporte":
                        ViewBag.Reporte = true;
                        break;

                    case "Solicitud":
                        ViewBag.Solicitud = true;
                        break;


                    case "Jerarquia":
                        ViewBag.Jerarquia = true;
                        break;
                    default:
                        break;
                }
            }

            return PartialView();
            
        }
    }
}

