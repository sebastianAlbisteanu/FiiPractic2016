using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TriangleWebApi.Models;

namespace TriangleWebApi.Controllers
{
    [RoutePrefix("api/triangles")]
    public class TriangleController : ApiController {
        private List<Triangle> _triangles = new List<Triangle> {
            new Triangle {A = 100, B = 200, C = 300 },
            new Triangle {A = 200, B = 300, C = 300 }
        };

        [HttpGet]
        [Route("all")]
        public List<Triangle> GetAllTriangles() {
            return _triangles;
        }

        [HttpGet]
        [Route("{edge:int}")]
        public IHttpActionResult GetTriangle(int edge) {
            var triangles = _triangles.Where(t => t.A == edge || t.B == edge || t.C == edge).ToList();
            if (triangles.Any()) {
                return Ok(triangles);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("calcPerimeter")]
        public IHttpActionResult CalculatePerimeter(Triangle triangle)
        {
            if (triangle != null)
            {
                return Ok(triangle.A + triangle.B + triangle.C);
            }
            return BadRequest("triangle was null");
        }
    }
}
