using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ConsumerWebApi.Models;
using Daishi.AMQP;
using RabbitMQ.Client.Events;

namespace ConsumerWebApi.Controllers
{
    public class ConsumerController : ApiController
    {
        [HttpGet]
        [Route("test")]
        public IHttpActionResult TestCallToTriangle()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49471/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Triangle triangle = new Triangle {A = 100, B = 200, C = 300};
                try
                {
                    var response = client.PostAsJsonAsync("api/triangles/calcPerimeter", triangle).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsAsync<int>().Result;

                        return Ok(result);
                    }

                    return
                        InternalServerError(
                            new Exception("something went wrong, response from triangle: " + response.StatusCode));
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
        }

        [HttpGet]
        [Route("testMicroservice")]
        public IHttpActionResult TestCallToMicroservice()
        {
            var rabbitMqAdapterInstance = RabbitMQAdapter.Instance;
            rabbitMqAdapterInstance.Init("localhost", 5672, "guest", "guest", 50);
            rabbitMqAdapterInstance.Connect();

            var rand = new Random();
            int e1 = rand.Next(1000);
            int e2 = rand.Next(1000);
            int e3 = rand.Next(1000);

            string edges = e1 + "/" + e2 + "/" + e3;
            rabbitMqAdapterInstance.Publish(edges, "TrianglePerimeter");

            string response;
            BasicDeliverEventArgs args;
            bool ok = false;
            bool responded;
            while (!ok)
            {
                responded = rabbitMqAdapterInstance
                    .TryGetNextMessage("TrianglePerimeterResult",
                        out response, out args, 5000);
                var responseSplit = response.Split('@');
                if (responseSplit.Length > 1 && responseSplit[1].Equals(edges))
                {
                    return Ok(response);
                }
                else
                {
                    rabbitMqAdapterInstance.Publish(response, "TrianglePerimeterResult");
                }
            }
            return InternalServerError();
        }

    }
}
