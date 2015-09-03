using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASKI_VACACIONES.Controllers
{
    public class DepartamentosController : Controller
    {
        // GET: Departamentos
        public ActionResult Index()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Index(DepartamentoModel model)
        {
            
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    client.addDepartamento(model.descripcion);
                   client.Close();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Edit(DepartamentoModel model, string submitButton)
        {
            Service1Client client = new Service1Client();
            switch (submitButton)
            {
                case "Buscar":
                    var hola = client.getDepartamento(model.id);
                    if (hola != null) { 
                    ViewBag.Desc = hola;
                    ViewBag.id = model.id;
                    }
                    client.Close();
                    return View();
                case "Modificar":
                    if (Session["User"] != null)
                    {
                        client.editDepartamento(model.id, model.descripcion);
                    }
                        client.Close();
                    
                    return View();
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
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
        public ActionResult Delete()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }
    }
}