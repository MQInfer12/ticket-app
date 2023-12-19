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

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var registers = _db.Cajas
                .Select(x => new CajaResponse
                {
                    Id = x.Id,
                    CajaName = x.Nombre,
                    CompanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre

                });
            return Ok(new BaseResponse<IQueryable<CajaResponse>>
            { Message = "Lista de contactos obtenida correctamente", Data = registers, Status = 200 });
        }

        [HttpGet("{GetByCompanyId}"), Authorize]
        public IActionResult GetByCompany(Guid GetByCompanyId)
        {
            var registers = _db.Cajas
                .Where(v => v.Idempresa == GetByCompanyId)
                .Select(x => new CajaResponse
                {
                    Id = x.Id,
                    CajaName = x.Nombre,
                    CompanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre

                });
            return Ok(new BaseResponse<IQueryable<CajaResponse>>
            { Message = "Lista de contactos obtenida correctamente", Data = registers, Status = 200 });
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

            var registersReturn = new CajaResponse
            {
                CajaName = registers.Nombre,
                CompanyId = registers.Idempresa,
            };

            return Ok(new BaseResponse<CajaResponse>
                { Message = "Se creo la empresa", Data = registersReturn, Status = 201 });
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, CajaDTO req)
        {

            var existingCaja= _db.Cajas.Find(id);

            if (existingCaja == null)
            {
                return NotFound(new { Message = "No se encontro la caja para editar", Data = ' ', Status = 404 });
            }

            existingCaja.Nombre = req.CajaName;
            existingCaja.Idempresa = req.CompanyId;

            _db.Cajas.Update(existingCaja);
            _db.SaveChanges();

            var registersReturn = new CajaResponse
            {
                CajaName = existingCaja.Nombre,
                CompanyId = existingCaja.Idempresa,
            };


            return Ok(new BaseResponse<CajaResponse>
                { Message = "Se edito la empresa", Data = registersReturn, Status = 200 });

        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var registers = _db.Cajas.Find(id);
            if (registers == null)
            {
                return NotFound(new { Message = "No se encontro la caja", Data = ' ', Status = 404 });
            }

            _db.Cajas.Remove(registers);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la Caja", Data = ' ', Status = 200});

        }
    }
}