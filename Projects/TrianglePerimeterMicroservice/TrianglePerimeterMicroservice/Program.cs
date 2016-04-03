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
            var microservice = new Microservice.TrianglePerimeterMicroservice();
            microservice.Init();
            Console.ReadLine();
        }
    }
}
