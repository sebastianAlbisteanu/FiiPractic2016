using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrianglePerimeterMicroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            var microservices = new List<Microservice.TrianglePerimeterMicroservice>();
            for (int i = 0; i < n; i++)
            {
                var microservice = new Microservice.TrianglePerimeterMicroservice();
                microservice.Init();
                microservices.Add(microservice);
            }

            Console.ReadLine();
        }
    }
}
