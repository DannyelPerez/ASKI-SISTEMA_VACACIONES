using ASKI_VACACIONES.Models;
using ASKI_VACACIONES.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASKI_VACACIONES.Controllers
{
    public class EventosController : Controller
    {
        // GET: Ccalendario
        public ViewResult Eventos()
        {
            if (Session["User"] != null)
                return View();
            else
                return View("Login");
        }

    

        [HttpPost]
        public ActionResult Eventos(EventosModel calendario)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Service1Client client = new Service1Client();
                    client.addTipo_dia(calendario.descripcion);
                    List<string> fecha = splitCadenaID(calendario.Fecha);
                    calendario.Tipo_dia_id = client.getultimoid_tipodia();
                    int talento = int.Parse(Session["Talento_Humano"].ToString());

                    foreach (var item in fecha)
                    {
                        string fechas = getFecha(item);
                        client.addCalendario(talento,fechas, calendario.Tipo_dia_id);
                   
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

        private List<string> splitCadenaID(string cadenaID)
        {
            List<string> cadena = new List<string>();
            cadenaID += ",";
            string valor = "";
            for (int i = 0; i < cadenaID.Length; i++)
            {
                if (cadenaID[i] == ',')
                {
                    cadena.Add(valor);
                    valor = "";
                }
                else
                {
                    if (cadenaID[i] != ' ')
                    {
                        valor += cadenaID[i];
                    }
                   
                }

            }
            return cadena;
        }

        private string getFecha(string fecha)
        {
            string fe = "";
            string dia = "", mes = "", year = "";
            int separador = 0, contYear = 0;
            for (int i = 0; i < fecha.Length; i++)
            {
                if (fecha[i] == '/')
                {
                    separador++;
                }
                else if (separador == 0)
                {
                    mes += fecha[i];
                }
                else if (separador == 1)
                {
                    dia += fecha[i];
                }
                else if (separador == 2 && contYear < 4)
                {
                    year += fecha[i];
                    contYear++;
                }
                else
                {
                    break;
                }

            }
            fe = year + "-" + mes + "-" + dia;
            return fe;
        }

        [HttpPost]
        public ActionResult JSonEvento()
        {
            string json = "";
            Service1Client client = new Service1Client();
            var query = client.get_eventos();
           
            if (query == null)
            {
                json += "{" + String.Format("\"descripcion\":\"{0}\",\"fecha\":\"{1}\"", "0", "Null") + "}";
                json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
                return Content(json);
            }
            for (int i = 0; i < query.Count(); i++)
            {
                var f = client.get_fecha_eventos(query.ElementAt(i));
                List<string> fechas = new List<string>();
                foreach (var item in f) {
                    fechas.Add(item);
                }
                string cadenaf = concatenar_fechas(fechas);
                if (!json.Equals("")) { json += ","; }
                json += "{" + String.Format("\"descripcion\":\"{0}\",\"fecha\":\"{1}\"", query.ElementAt(i), cadenaf) + "}";
            }

            json = "{\"draw\": 1,\"recordsTotal\": 1,\"recordsFiltered\": 1,\"data\": [" + json + "]}";
            return Content(json);
        }

        private string concatenar_fechas( List<string> fechas)
        {
            string fecha = "";
            foreach (var item in fechas)
            {
                fecha += item + " ";
            }
            return fecha;
        }

    }
}