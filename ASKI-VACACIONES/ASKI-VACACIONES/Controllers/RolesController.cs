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
                return RedirectToAction("Login", "Home");
        }
         
         private List<int> splitCadenaID(string cadenaID) {
            List<int> numero = new List<int>();
            string valor = "";
            for (int i = 0; i < cadenaID.Length; i++)
            {
                if (cadenaID[i] == '/')
                {
                    numero.Add(int.Parse(valor));
                    valor = "";
                }
                else
                {
                    valor += cadenaID[i];
                }

            }
            return numero;
        }

        [HttpPost]
        public ActionResult Edit(RolesModel model, string submitButton)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    switch (submitButton)
                    {
                        case "Buscar":
                            var hola = client.getRol(model.id);
                            if (hola == null)
                            {
                                client.Close();
                                return View();
                            }
                            ViewBag.Desc = hola.descripcion;
                            ViewBag.id = hola.rolesid;
                            break;
                        case "Modificar":
                            client.editRol(model.id, model.descripcion);
                            break;
                    }
                    client.Close();
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        public ActionResult Edit()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Index(RolesModel model)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    List<int> idpermisos = splitCadenaID(model.permisosID);
                    Session["Roles"] = model.descripcion;
                    ViewBag.rol = Session["Roles"];
                    if(idpermisos==null)
                        return View();
                    client.addRole(model.descripcion);
                    foreach (var item in idpermisos)
                    {
                        client.addRoles_Permisos(client.getUltimoId_Roles(),item);
                    }
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
        public ActionResult JSonRoles()
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getTbl_permisos();
            if (query == null)
            {
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", "0", "Null") + "}";
                json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
                return Content(json);
            }
            for (int i = 0; i < query.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", query.ElementAt(i).permisosid, query.ElementAt(i).descripcion) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }
      }
}