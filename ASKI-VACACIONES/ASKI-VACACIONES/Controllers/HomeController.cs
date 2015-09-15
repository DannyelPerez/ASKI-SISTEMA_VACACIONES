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
        public ActionResult Perfil() 
        {
            if (Session["User"] != null)
                return View();
            else
                return View("Login");
        }
        public ViewResult Calendario() 
        { 
            if(Session["User"]!=null)
            return View(); 
            else 
           return View("Login");
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
                    if (aceder == null)
                        return View("Login");

                    Session["User"] = user.email;
                    return RedirectToAction("AfterLogin");
                }
                catch(Exception ex)
                {
                    return View("Login");
                }
                
            }
            return View("Login");
        }

        public ActionResult AfterLogin()
        {
            if(Session["User"]!=null)
            {
                return RedirectToAction("Calendario");
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
       
    }
}