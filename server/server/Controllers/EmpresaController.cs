using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmpresaController: ControllerBase
    {
        private DBContext _db;
        public EmpresaController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cars = _db.Empresas;
            return Ok(cars);
        }
    }
}
