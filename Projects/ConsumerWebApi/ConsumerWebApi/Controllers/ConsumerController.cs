using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ConsumerWebApi.Models;

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
    }
}
