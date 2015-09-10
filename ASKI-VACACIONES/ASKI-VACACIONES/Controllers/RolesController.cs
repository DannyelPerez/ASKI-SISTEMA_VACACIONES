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
                    var hola = client.getRol(model.id);
                    if (hola != null) { 
                    ViewBag.Desc = hola.descripcion;
                    ViewBag.id = hola.rolesid;
                    }
            
                    return View();
                case "Modificar":
                    if (Session["User"] != null)
                    {

                        client.editRol(model.id, model.descripcion);
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
            client.addRole(model.descripcion);
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

        public ActionResult JSonRoles()
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getTbl_permisos();
            List<PermisosModel> permisos = new List<PermisosModel>();
            foreach (var item in query)
            {
                PermisosModel  roles_permisos= new PermisosModel();
                roles_permisos.id = item.permisosid;
                roles_permisos.descripcion = item.descripcion;
                permisos.Add(roles_permisos);
            }
            for (int i = 0; i < permisos.Count; i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", permisos.ElementAt(i).id, permisos.ElementAt(i).descripcion) + "}";


            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

    }
}