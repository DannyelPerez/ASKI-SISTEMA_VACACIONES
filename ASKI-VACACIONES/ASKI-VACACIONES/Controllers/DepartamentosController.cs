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
                return RedirectToAction("Login", "Home");
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
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(DepartamentoModel model, string submitButton)
        {

            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    switch (submitButton)
                    {
                        case "Buscar":
                            var hola = client.getDepartamento(model.id);
                            if (hola == null)
                            {
                                client.Close();
                                return View();
                            }
                            ViewBag.Desc = hola.descripcion;
                            ViewBag.id = hola.departamentoid;
                            break;
                        case "Modificar":
                            client.editDepartamento(model.id, model.descripcion);
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

        public ActionResult Edit()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult Delete()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }
    }
}