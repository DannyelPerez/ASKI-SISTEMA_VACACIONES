﻿using ASKI_VACACIONES.Models;
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

    }
}