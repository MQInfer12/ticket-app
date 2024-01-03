using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CajaController : Controller
    {
        private DBContext _db;

        public CajaController(DBContext db)
        {
            _db = db;
        }

        //functions that we use in several endpoints
        private IActionResult NotfoundFunc()
        {
            return NotFound(new { Message = "No se encontro ningun dato", Data = ' ', Status = 404 });
        }


        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var rol = User.FindFirst("RoleName").Value;
            IQueryable<CajaResponse> registers;
            if (rol == "Super Administrador")
            {
                registers = _db.Cajas
                   .Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                   ));
            }
            else
            {
                var idEmpresa = User.FindFirst("CompanyId").Value;
                registers = _db.Cajas.Where(c => c.Idempresa == Guid.Parse(idEmpresa))
               .Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                   ));
            }
            return Ok(new BaseResponse<IQueryable<CajaResponse>>
            { Message = "Lista de cajas obtenida correctamente", Data = registers, Status = 200 });
        }

        [HttpGet("{GetByCompanyId}"), Authorize]
        public IActionResult GetByCompany(Guid GetByCompanyId)
        {
            var registers = _db.Cajas
                .Where(v => v.Idempresa == GetByCompanyId)
          .Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                   ));
            return Ok(new BaseResponse<IQueryable<CajaResponse>>
            { Message = "Lista de contactos obtenida correctamente", Data = registers, Status = 200 });
        }

        [HttpGet, Authorize]
        [Route("ById/{id}")]
        public IActionResult Get(Guid id)
        {
            var caja = _db.Cajas.Where(c => c.Id == id).Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                   )).FirstOrDefault();

            if (caja == null)
            {
                return NotfoundFunc();
            }

            return Ok(new { Message = "Datos obtenidos con exito", Data = caja, Status = 200 });
        }


        [HttpPost, Authorize]
        public IActionResult Post([FromBody] CajaDTO req)
        {

            var registers = new Caja
            {
                Nombre = req.CajaName,
                Idempresa = req.CompanyId
            };

            _db.Cajas.Add(registers);
            _db.SaveChanges();

            var registersReturn = _db.Cajas
                .Where(v => v.Id == registers.Id)
                 .Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                   )).First();

            return Ok(new BaseResponse<CajaResponse>
            { Message = "Se creo la empresa", Data = registersReturn, Status = 201 });
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, CajaDTO req)
        {

            var existingCaja = _db.Cajas.Find(id);

            if (existingCaja == null)
            {
                return NotfoundFunc();
            }

            existingCaja.Nombre = req.CajaName;
            existingCaja.Idempresa = req.CompanyId;

            _db.Cajas.Update(existingCaja);
            _db.SaveChanges();

            var registersReturn = _db.Cajas
                .Where(v => v.Id == existingCaja.Id)
                   .Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                   )).First();

            return Ok(new BaseResponse<CajaResponse>
            { Message = "Se edito la empresa", Data = registersReturn, Status = 200 });

        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var registers = _db.Cajas.Find(id);
            if (registers == null)
            {
               return NotfoundFunc();
            }

            _db.Cajas.Remove(registers);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la Caja", Data = ' ', Status = 200 });

        }
    }
}