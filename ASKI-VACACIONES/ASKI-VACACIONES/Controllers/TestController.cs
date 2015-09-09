using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ASKI_VACACIONES.Models;

namespace ASKI_VACACIONES.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult JSon()
        {
           string json = "";
            Service1Client client = new Service1Client();
            var query = client.getTbl_usuarios();
            List<UsuariosModel> user = new List<UsuariosModel>();
            foreach (var item in query)
            {
                UsuariosModel u = new UsuariosModel();
                u.activo = item.activo;
                u.email = item.email;
                u.fecha_creacion = item.fecha_creacion;
                u.fecha_ingreso = item.fecha_ingreso;
                u.primer_apellido = item.primer_apellido;
                u.primer_nombre = item.primer_nombre;
                u.segundo_apellido = item.segundo_apellido;
                u.segundo_nombre = item.segundo_nombre;
                u.talento_humano = item.talento_humano;
                user.Add(u);
            }
            for (int i = 0; i < user.Count; i++)
            {
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"talento_humano\":\"{0}\",\"primer_nombre\":\"{1}\",\"primer_apellido\":\"{2}\",\"option\":\"{3}\"", user.ElementAt(i).talento_humano, user.ElementAt(i).primer_nombre, user.ElementAt(i).primer_apellido, 1) + "}";
                
                
            }

             json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": ["+json+"]}";
            return Content(json);
        }
    }
}