using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TriangleWebApi.Models;

namespace TriangleWebApi.Controllers
{
    public class TriangleController : ApiController {
        private List<Triangle> _triangles = new List<Triangle> {
            new Triangle {A = 100, B = 200, C = 300 },
            new Triangle {A = 200, B = 300, C = 300 }
        };

        public List<Triangle> GetAllTriangles() {
            return _triangles;
        }

        public IHttpActionResult GetTriangle(int id) {
            var triangles = _triangles.Where(t => t.A == id || t.B == id || t.C == id).ToList();
            if (triangles.Any()) {
                return Ok(triangles);
            }
            return NotFound();
        }
    }
}
