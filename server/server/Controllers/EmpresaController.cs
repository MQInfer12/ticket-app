using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

       /* [Authorize]*/
        [HttpGet, Authorize(Roles ="SuperAdmin")]
        public IActionResult Get()
        {
            var companies = _db.Empresas;
            return Ok(new { Message = "Datos obtenidos con exito", Data = companies, Status = 200 });
        }

        [AllowAnonymous]
        [HttpGet("{id}"), Authorize]
        public IActionResult Get(Guid id)
        {
            var company = _db.Empresas.Find(id);

            if (company == null)
            {
                return NotFound(new { Message = "No existe esta empresa", Data = ' ', Status = 404 });
            }

            return Ok(new { Message = "Datos obtenidos con exito", Data = company, Status = 200 });
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] EmpresaDTO req)
        {
            var exists = _db.Empresas.Any(e => e.Nombre == req.Nombre);
            if (exists)
            {
                return BadRequest(new { Message = "Ya existe esta empresa", Data = ' ', Status = 400 });
            }

            var company = new Empresa
            {
                Nombre = req.Nombre,
                Direccion = req.Direccion,
                Estado = req.Estado
            };

            _db.Empresas.Add(company);
            _db.SaveChanges();

            return Ok(new { Message = "Se creo la empresa", Data = company, Status = 201 });
        }


        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, EmpresaDTO req)
        {

            var existingEmpresa = _db.Empresas.Find(id);

            if (existingEmpresa == null)
            {
                return NotFound(new { Message = "No se encontro la empresa", Data = ' ', Status = 404 });
            }

            existingEmpresa.Nombre = req.Nombre;
            existingEmpresa.Direccion = req.Direccion;
            existingEmpresa.Estado = req.Estado;

            _db.Empresas.Update(existingEmpresa);
            _db.SaveChanges();
            return Ok(new { Message = "Se edito la empresa", Data = existingEmpresa, Status = 200 });

        }


        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var company = _db.Empresas.Find(id);
            if (company == null)
            {
                return NotFound(new { Message = "No se encontro la empresa", Data = ' ', Status = 404 });
            }

            _db.Empresas.Remove(company);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la empresa", Data = ' ', Status = 200});

        }

    }
}
