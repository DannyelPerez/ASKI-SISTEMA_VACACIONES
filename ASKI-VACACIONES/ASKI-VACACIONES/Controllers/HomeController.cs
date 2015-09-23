using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login(){return View();}
        public ActionResult Bienvenida() { return View(); }
        public ActionResult Perfil() 
        {
            if (Session["User"] != null)
                return View();
            else
                return View("Login");
        }
        [HttpPost]
        public ActionResult Perfil(UsuariosModel model, string submitButton)
        {
            if (Session["User"] != null)
            {
                Service1Client client = new Service1Client();
                if (submitButton.Equals("Perfil"))
                {
                    var aceder = client.getUsuario(int.Parse(Session["Talento_Humano"].ToString()));
                    UsuariosModel usuario = new UsuariosModel();
                    if (aceder == null)
                    {
                        client.Close();
                        return View();
                    }
                    setSessionVar(aceder.email, aceder.primer_nombre, aceder.primer_apellido, aceder.segundo_nombre, aceder.segundo_apellido);
                    ViewBag.email = Session["Email"];
                    ViewBag.primerNombre = Session["Primer_nombre"];
                    ViewBag.segundoNombre = Session["Segundo_nombre"];
                    ViewBag.primerApellido = Session["Primer_apellido"];
                    ViewBag.segundoApellido = Session["Segundo_apellido"];

                    
                   
                }
                else if (submitButton.Equals("Cambios"))
                {
                    if (ModelState.IsValid)
                    {
                        client.perfil(int.Parse(Session["Talento_Humano"].ToString()), model.primer_nombre, model.segundo_nombre, model.primer_apellido, model.segundo_apellido, model.email);
                       
                     }
                }
                client.Close();
                return View();

            }
            else
                return RedirectToAction("Login", "Home");
          
        }
       
        public ViewResult Ayuda() 
        {
            if (Session["User"] != null)
                return View();
            else
                return View("Login");
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Service1Client client = new Service1Client();
                    var aceder = client.confirmarLogin(user.email, user.password);
                    var tempo = client.getLista_Permisos(aceder.talento_humano);
                    Session["Permisos"]=client.getLista_Permisos(aceder.talento_humano);
                    if (aceder == null)
                        return View("Login");
                    Session["User"] = aceder.primer_nombre + " " + aceder.primer_apellido;
                    ViewBag.user = Session["User"];
                    Session["Pass"] = aceder.password;
                    Session["Talento_Humano"] = aceder.talento_humano;
                    setSessionVar(aceder.email, aceder.primer_nombre, aceder.primer_apellido, aceder.segundo_nombre, aceder.segundo_apellido);

                    return RedirectToAction("AfterLogin");
                }
                catch(Exception ex)
                {
                    return View("Login");
                }
                
            }
            return View("Login");
        }

        private void setSessionVar(string email, string primer_nombre, string primer_apellido, string segundo_nombre, string segundo_apellido)
        {
            
            Session["Primer_nombre"] = primer_nombre;
            Session["Primer_apellido"] = primer_apellido;
            Session["Segundo_nombre"] = segundo_nombre;
            Session["Segundo_apellido"] = segundo_apellido;
            Session["Email"] = email;

        }

        public ActionResult AfterLogin()
        {
            if(Session["User"]!=null)
            {
                return RedirectToAction("Bienvenida");
            }
            else
            {
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["User"] = null;
            Session.Abandon();
            return View("Login");
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
       
    }
}