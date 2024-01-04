using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;
using server.Constants;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CajaController : Controller
    {
        private DBContext _db;

        HttpClass<CajaResponse> _httpInstance = new HttpClass<CajaResponse>();
        public CajaController(DBContext db)
        {
            _db = db;
        }

        private string UserToken(string val)
        {
            return User.FindFirst(val).Value;
        }

        private CajaResponse getCajaResponse(Guid? id = null)
        {
            var caja = _db.Cajas.Where(c => c.Id == id).Select(x => new CajaResponse(
                 x.Id,
                 x.Idempresa,
                 x.Nombre,
                 x.IdempresaNavigation.Nombre
             )).First();
            return caja;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            string rol = UserToken("RoleName");
            IQueryable<CajaResponse> registers;

            if (rol == Roles.SuperAdmin)
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
                var idEmpresa = UserToken("CompanyId");
                registers = _db.Cajas
                    .Where(c => c.Idempresa == Guid.Parse(idEmpresa))
                    .Select(x => new CajaResponse(
                       x.Id,
                       x.Idempresa,
                       x.Nombre,
                       x.IdempresaNavigation.Nombre
                    ));
            }

            return Ok(_httpInstance.Get(registers));

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

            HttpClass<CajaResponse> _httpInstance = new HttpClass<CajaResponse>();
            return Ok(_httpInstance.Get(registers));

        }

        [HttpGet, Authorize]
        [Route("ById/{id}")]
        public IActionResult Get(Guid id)
        {
            var caja = getCajaResponse(id);

            if (caja == null)
            {
                return NotFound(_httpInstance.NotfoundFunc());
            }

            return Ok(_httpInstance.GetJustOne(caja));
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

            var registersReturn = getCajaResponse(registers.Id);

            return Ok(_httpInstance.Post(registersReturn));
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, CajaDTO req)
        {

            var existingCaja = _db.Cajas.Find(id);

            if (existingCaja == null)
            {
                return NotFound(_httpInstance.NotfoundFunc());
            }

            existingCaja.Nombre = req.CajaName;
            existingCaja.Idempresa = req.CompanyId;

            _db.Cajas.Update(existingCaja);
            _db.SaveChanges();

            var registersReturn = getCajaResponse(existingCaja.Id);

            return Ok(_httpInstance.Put(registersReturn));

        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var registers = _db.Cajas.Find(id);
            if (registers == null)
            {
                return NotFound(_httpInstance.NotfoundFunc());
            }

            _db.Cajas.Remove(registers);
            _db.SaveChanges();

            return Ok(_httpInstance.Delete());

        }
    }
}