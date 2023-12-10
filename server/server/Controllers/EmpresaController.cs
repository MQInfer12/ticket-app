using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmpresaController : ControllerBase
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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var company = _db.Empresas.Find(id);

            if (company == null)
            {
                return Ok(new { Message = "No existe esta empresa" });
            }

            return Ok(company);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmpresaDTO req)
        {
            var exists = _db.Empresas.Any(e => e.Nombre == req.Nombre);
            if (exists)
            {
                return Ok(new { Message = "Ya existe esta empresa" });
            }

            var company = new Empresa
            {
                Nombre = req.Nombre,
                Direccion = req.Direccion,
                Estado = req.Estado
            };

            _db.Empresas.Add(company);
            _db.SaveChanges();

            return Ok(new { Message = "Se creo la empresa", Data = company });
        }


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, EmpresaDTO req)
        {

            var existingEmpresa = _db.Empresas.Find(id);

            if (existingEmpresa == null)
            {
                return NotFound();
            }

            existingEmpresa.Nombre = req.Nombre;
            existingEmpresa.Direccion = req.Direccion;
            existingEmpresa.Estado = req.Estado;

            _db.Empresas.Update(existingEmpresa);
            _db.SaveChanges();
            return Ok(new { Message = "Se edito la empresa", Data = existingEmpresa });

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {

            var company = _db.Empresas.Find(id);
            if (company == null)
            {
                return NotFound(new
                {
                    Message = "No se encontro la empresa"
                });
            }

            _db.Empresas.Remove(company);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la empresa" });

        }

    }
}
