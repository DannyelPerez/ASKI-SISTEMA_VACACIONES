using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Index(UsuariosModel model)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    List<int> idDepartamentos = splitCadenaID(model.departamentosID);
                    List<int> idRoles = splitCadenaID(model.rolesID);
                    if(idDepartamentos==null)
                        return View();
                    if(idRoles==null)
                        return View();
                    client.addUsuario(model.talento_humano, model.email, model.primer_nombre, model.segundo_nombre, model.primer_apellido, model.segundo_apellido, model.fecha_ingreso, model.password);
                    foreach (var item in idDepartamentos)
                    {
                        client.addUsuario_Departamento(model.talento_humano, item);
                    }
                    foreach (var item in idRoles)
                    {
                        client.addUsuario_Rol(model.talento_humano, item);
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
        public ActionResult Edit()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Edit(UsuariosModel model, string submitButton)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    switch (submitButton)
                    {
                        case "Buscar":
                            var hola = client.getUsuario(model.talento_humano);
                            UsuariosModel usuario = new UsuariosModel();
                            if (hola == null)
                            {
                                client.Close();
                                return View();
                            }
                            ViewBag.email = hola.email;
                            ViewBag.talentoHumano = hola.talento_humano;
                            ViewBag.primerNombre = hola.primer_nombre;
                            ViewBag.segundoNombre = hola.segundo_nombre;
                            ViewBag.primerApellido = hola.primer_apellido;
                            ViewBag.segundoApellido = hola.segundo_apellido;
                            ViewBag.fechaIngreso = hola.fecha_creacion;
                            ViewBag.fecha_ingreso = hola.fecha_ingreso;
                            break;
                        case "Modificar":
                            client.editUsuario(model.talento_humano, model.email, model.primer_nombre, model.segundo_nombre, model.primer_apellido, model.segundo_apellido, model.fecha_ingreso);
                            client.deleteRoles_Usuarios(model.talento_humano);
                        client.deleteDepartamento_Usuarios(model.talento_humano);
                        var seleccionados=client.getIdsRoles_Usuario(model.talento_humano);
                        //Rebuild Table
                        List<int> idDepartamentos = splitCadenaID(model.departamentosID);
                        foreach (var item in idDepartamentos)
                        {
                            client.addUsuario_Departamento(model.talento_humano, item);
                        }

                        List<int> idRoles = splitCadenaID(model.rolesID);
                        foreach (var item in idRoles)
                        {
                            client.addUsuario_Rol(model.talento_humano, item);
                        }

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
        public ActionResult Delete()
        {
            if (Session["User"] != null)
                return View();
            else
                return RedirectToAction("Login","Home");
        }

        public ActionResult JSonDepartamentos()
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getTbl_departamentos();
            if(query==null)
            {
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", "0", "Null") + "}";
                json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
                return Content(json);
            }
            for (int i = 0; i < query.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", query.ElementAt(i).departamentoid, query.ElementAt(i).descripcion) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

        public ActionResult JSonRoles()
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getTbl_roles();
            if (query == null)
            {
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", "0", "Null") + "}";
                json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
                return Content(json);
            }
            for (int i = 0; i < query.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"id\":\"{0}\",\"descripcion\":\"{1}\"", query.ElementAt(i).rolesid, query.ElementAt(i).descripcion) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

        private List<int> splitCadenaID(string cadenaID)
        {
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

        public ActionResult JSonGetActiveRoles(int id)
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getIdsRoles_Usuario(id);
            for (int i = 0; i < query.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"id\":\"{0}\"", query.ElementAt(i)) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

        public ActionResult JSonGetActiveDepartementos(int id)
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.getIdDepartamentos_Usuario(id);
            for (int i = 0; i < query.Count(); i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"id\":\"{0}\"", query.ElementAt(i)) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }



    }
}