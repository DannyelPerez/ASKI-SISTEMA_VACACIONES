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
                return RedirectToAction("Login", "Home");
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
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Edit()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Edit(PermisosModel model, string submitButton)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    switch (submitButton)
                    {
                        case "Buscar":
                            var hola = client.getPermiso(model.id);
                            if (hola == null)
                            {
                                client.Close();
                                return RedirectToAction("Login", "Home");
                            }
                            ViewBag.Desc = hola.descripcion;
                            break;
                        case "Modificar":
                            client.editPermiso(model.id, model.descripcion, model.activo);
                            break;

                    }
                    client.Close();
                    return View();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Delete(PermisosModel model)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    client.deletePermiso(model.id);
                    client.Close();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Delete()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Cargar(PermisosModel model)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    var dic = client.getPermiso(model.id);
                    client.Close();
                    return View(dic);
                }
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
    }
}