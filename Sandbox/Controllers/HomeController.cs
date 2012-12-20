using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PaymillWrapper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modifique esta plantilla para poner en marcha su aplicación ASP.NET MVC.";

            Paymill.ApiKey = "9a4129b37640ea5f62357922975842a1";
            HttpClient client = Paymill.Client;

            /*
            // listado de clientes
            var task = client.GetAsync(Paymill.ApiUrl + "/clients").ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;
                            var lstOffers = new List<Client>();
                            lstOffers = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<List<Client>>(jsonArray["data"].ToString());

                            foreach (Client cliente in lstOffers)
                            {
                                Console.WriteLine("Name: {0}", cliente.Description);
                                int i = 0;
                            }

                        });
                });*/

            /*
            // nuevo cliente
            Client c = new Client();
            c.Description = "Prueba";
            c.Email="javicantos@hotmail.es";

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(c).ToLower();
            HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            task = client.PostAsync(Paymill.ApiUrl + "/clients", content).ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;

                            Client cteNuevo = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<Client>(jsonArray["data"].ToString());

                            Console.WriteLine("Name: {0}", cteNuevo.Id);
                            Console.WriteLine("Name: {0}", cteNuevo.Email);

                        });
                });*/

            // nueva oferta
            Offer off = new Offer();
            off.Amount = 4200;
            off.Currency = "eur";
            off.Interval = Offer.TypeInterval.WEEK;
            off.Name = "Oferta";
            off.Trial_Period_Days = 3;

            string peticion = new URLEncoder().Encode<Offer>(off);
            var content = new StringContent(peticion);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
 
            var task = client.PostAsync(Paymill.ApiUrl + "/offers", content).ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;

                            Offer cteNuevo = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<Offer>(jsonArray["data"].ToString());

                            Console.WriteLine("Name: {0}", cteNuevo.Id);
                            Console.WriteLine("Name: {0}", cteNuevo.Name);

                        });
                });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Página de descripción de la aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de contacto.";

            return View();
        }
    }
}
