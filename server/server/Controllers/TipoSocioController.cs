using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoSocioController : Controller
    {
        private DBContext _db;

        public TipoSocioController(DBContext db)
        {
            _db = db;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var partnerType = _db.TipoSocios
                .Select(x => new TipoSocioResponse
                {
                    Id = x.Id,
                    TypePartnerName = x.Nombre,
                    TypePartnerCost = x.Costo,
                    TypePartnerDiscount = x.Descuento,
                    TypePartnerDuration = x.Duracion,
                    ComapanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre
                });
            return Ok(new BaseResponse<IQueryable<TipoSocioResponse>>
            { Message = "Datos obtenidos correctamente", Data = partnerType, Status = 200 });
        }

        [HttpGet("{GetByCompanyId}"), Authorize]
        public IActionResult GetByCompany(Guid GetByCompanyId)
        {
            var partnertsType = _db.TipoSocios
                .Where(v => v.Idempresa == GetByCompanyId)
                .Select(x => new TipoSocioResponse
                {
                    Id = x.Id,
                    TypePartnerName = x.Nombre,
                    TypePartnerCost = x.Costo,
                    TypePartnerDiscount = x.Descuento,
                    TypePartnerDuration = x.Duracion,
                    ComapanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre
                });
            return Ok(new BaseResponse<IQueryable<TipoSocioResponse>>
            { Message = "Datos obtenidos correctamente", Data = partnertsType, Status = 200 });
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] TipoSocioDTO req)
        {
            var findCompany = _db.Empresas.Find(req.CompanyId);

            if (findCompany == null)
            {
                return NotFound(new BaseResponse<string>
                { Message = "No se encontro la empresa", Data = " ", Status = 404 });
            }

            var PartnerType = new TipoSocio
            {
                Costo = req.Cost,
                Descuento = req.Discount,
                Duracion = req.Duration,
                Estado = true,
                Idempresa = req.CompanyId,
                Nombre = req.Name,
                Partidos = req.Games,
            };

            _db.TipoSocios.Add(PartnerType);
            _db.SaveChanges();


            var userRegisterReturn = _db.TipoSocios
                .Where(v => v.Id == PartnerType.Id)
                .Select(x => new TipoSocioResponse
                {
                    Id = x.Id,
                    TypePartnerName = x.Nombre,
                    TypePartnerCost = x.Costo,
                    TypePartnerDiscount = x.Descuento,
                    TypePartnerDuration = x.Duracion,
                    ComapanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre
                }).First();

            return Ok(new BaseResponse<TipoSocioResponse>
            { Message = "Se realizo la operacion con exito", Data = userRegisterReturn, Status = 201 });
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, [FromBody] TipoSocioDTO req)
        {

            var existePartnetType = _db.TipoSocios.Find(id);

            if (existePartnetType == null)
            {
                return NotFound(new { Message = "No se encontraron los datos", Data = ' ', Status = 404 });
            }

            existePartnetType.Costo = req.Cost;
            existePartnetType.Descuento = req.Discount;
            existePartnetType.Duracion = req.Duration;
            existePartnetType.Estado = req.State;
            existePartnetType.Idempresa = req.CompanyId;
            existePartnetType.Nombre = req.Name;
            existePartnetType.Partidos = req.Games;


            _db.TipoSocios.Update(existePartnetType);
            _db.SaveChanges();


            var userRegisterReturn = _db.TipoSocios
                .Where(v => v.Id == existePartnetType.Id)
                .Select(x => new TipoSocioResponse
                {
                    Id = x.Id,
                    TypePartnerName = x.Nombre,
                    TypePartnerCost = x.Costo,
                    TypePartnerDiscount = x.Descuento,
                    TypePartnerDuration = x.Duracion,
                    ComapanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre
                }).First();

            return Ok(new BaseResponse<TipoSocioResponse>
            { Message = "Se realizo la operacion con exito", Data = userRegisterReturn, Status = 201 });
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var partnetType = _db.TipoSocios.Find(id);
            if (partnetType == null)
            {
                return NotFound(new { Message = "No se encontro ningun dato", Data = ' ', Status = 404 });
            }

            _db.TipoSocios.Remove(partnetType);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la informacion", Data = ' ', Status = 200 });

        }

    }
}