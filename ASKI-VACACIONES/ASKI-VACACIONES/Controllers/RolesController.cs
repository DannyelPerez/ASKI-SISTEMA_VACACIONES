using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Edit(RolesModel model, string submitButton)
        {
            Service1Client client = new Service1Client();
            switch (submitButton)
            {
                case "Buscar":
                    //VSys_AskiDBContext db = new VSys_AskiDBContext();
                    //string hola = client.getRolesInfo(model.id);
                    var context = new VSys_AskiDBContext();
                    foreach (UsuariosModel u in context.Usuarios)
                    {
                        ViewBag.Desc = u.primer_nombre;
                    }
                    //ViewBag.Desc = hola.primer_nombre;
                    //ViewBag.id = model.id;
                    client.Close();
                    return View();
                case "Modificar":
                    if (Session["User"] != null)
                    {
                        // var dic = client.getPermisosInfo(model.id);
                        //Session["Name"] = dic.descripcion;
                        client.editRoles(model.id, model.descripcion);
                        client.Close();
                    }
                    return View();
                default:
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
        public ActionResult Index(RolesModel model)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
            Service1Client client = new Service1Client();
            client.addRoles(model.descripcion);
            client.Close();
             }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
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