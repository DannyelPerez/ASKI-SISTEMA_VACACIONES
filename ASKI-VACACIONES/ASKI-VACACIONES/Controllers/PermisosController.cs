using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{

    
    public class PermisosController : Controller
    {
        // GET: Permisos
        public ActionResult Index()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Index(PermisosModel model)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    client.addPermiso(model.descripcion);
                    client.Close();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Edit()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Edit(PermisosModel model, string submitButton)
        {
            Service1Client client = new Service1Client();
            switch (submitButton)
            {
            case "Buscar":
           var hola = client.getPermiso(model.id);
            if (hola != null)
            {
                ViewBag.Desc = hola.descripcion;
            }
            client.Close();
            return View();     
                case "Modificar":
                    if (Session["User"] != null)
                    {
                        // var dic = client.getPermisosInfo(model.id);
                        //Session["Name"] = dic.descripcion;
                        client.editPermiso(model.id, model.descripcion, model.activo);
                        client.Close();
                    }
                    return View();
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Login");
            }

            
           
        }

        [HttpPost]
        public ActionResult Delete(PermisosModel model)
        {
            Service1Client client = new Service1Client();
            client.deletePermiso(model.id);
            client.Close();
            return View();
        }
        public ActionResult Delete()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Cargar(PermisosModel model)
        {
            Service1Client client = new Service1Client();
            var dic = client.getPermiso(model.id);
            client.Close();
            return View(dic);
        }
    }
}