using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;
using System.ComponentModel.Design;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        private DBContext _db;
        public EventTypeController(DBContext db)
        {
            _db = db;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var rol = User.FindFirst("RoleName").Value;
            IQueryable<EventTypeResponse> eventsType;
            if (rol == "Super Administrador")
            {
                eventsType = _db.TipoEventos
                    .Select(x => new EventTypeResponse
                    {
                        Id = x.Id,
                        TypeEventName = x.Nombre,
                        Date = x.Fecha,
                        Amount = x.Cantidad,
                        CompanyId = x.Idempresa,
                        CompanyName = x.IdempresaNavigation.Nombre
                    });
            } else{
                var idEmpresa = User.FindFirst("CompanyId").Value;
                eventsType = _db.TipoEventos.Where(e => e.Idempresa == Guid.Parse(idEmpresa))
                    .Select(x => new EventTypeResponse
                    {
                        Id = x.Id,
                        TypeEventName = x.Nombre,
                        Date = x.Fecha,
                        Amount = x.Cantidad,
                        CompanyId = x.Idempresa,
                        CompanyName = x.IdempresaNavigation.Nombre
                    });
            }

            if (eventsType == null)
            {
                return Ok(new BaseResponse<string> { Message = "No se encontraron datos", Data = " ", Status = 404 });
            }

            return Ok(new BaseResponse<IQueryable<EventTypeResponse>>
            {
                Message = "Datos obtenidos con exito",
                Data = eventsType,
                Status = 200
            });
        }

        [AllowAnonymous]
        [HttpGet("GetEventByEventTypeId{eventTypeId}"), Authorize]
        public IActionResult Get(Guid eventTypeId)
        {
            var eventType = _db.TipoEventos
                .Where(v => v.Id == eventTypeId)
                .Select(x => new EventTypeResponse
                {
                    Id = x.Id,
                    TypeEventName = x.Nombre,
                    Date = x.Fecha,
                    Amount = x.Cantidad,
                    CompanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre
                }).First();

            if (eventType == null)
            {
                return NotFound(new { Message = "No existe esta tipo de evento", Data = ' ', Status = 404 });
            }

            return Ok(new { Message = "Datos obtenidos con exito", Data = eventType, Status = 200 });
        }

        [AllowAnonymous]
        [HttpGet("GetEventByCompanyId{companyId}"), Authorize]
        public IActionResult GetEventTypeByCompanyId(Guid companyId)
        {
            var eventType = _db.TipoEventos
                .Where(v => v.Idempresa == companyId)
                .Select(x => new EventTypeResponse
                {
                    Id = x.Id,
                    TypeEventName = x.Nombre,
                    Date = x.Fecha,
                    Amount = x.Cantidad,
                    CompanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre
                });

            if (eventType == null)
            {
                return NotFound(new { Message = "No existe esta tipo de evento", Data = ' ', Status = 404 });
            }

            return Ok(new { Message = "Datos obtenidos con exito", Data = eventType, Status = 200 });
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] EventTypeDTO req)
        {

            var eventType = new TipoEvento
            {
                Nombre = req.Nombre,
                Idempresa = req.Idempresa,
                Fecha = req.Fecha,
                Cantidad = req.Cantidad
            };

            _db.TipoEventos.Add(eventType);
            _db.SaveChanges();
            var eventTypeRes = _db.TipoEventos
                .Where(v => v.Id == eventType.Id)
                .Select(x => new EventTypeResponse
                {
                    Id = x.Id,
                    TypeEventName = x.Nombre,
                    Date = x.Fecha,
                    Amount = x.Cantidad,
                    CompanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre,
                }).First();

            return Ok(new { Message = "Se creo el tipo de evento", Data = eventTypeRes, Status = 201 });
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, EventTypeDTO req)
        {

            var existingEventType = _db.TipoEventos.Find(id);

            if (existingEventType == null)
            {
                return NotFound(new { Message = "No se encontro el tipo de evento", Data = ' ', Status = 404 });
            }

            existingEventType.Nombre = req.Nombre;
            existingEventType.Idempresa = req.Idempresa;
            existingEventType.Fecha = req.Fecha;
            existingEventType.Cantidad = req.Cantidad;

            _db.TipoEventos.Update(existingEventType);
            _db.SaveChanges();

            var eventTypeRes = _db.TipoEventos
                .Where(v => v.Id == existingEventType.Id)
                .Select(x => new EventTypeResponse
                {
                    Id = x.Id,
                    TypeEventName = x.Nombre,
                    Date = x.Fecha,
                    Amount = x.Cantidad,
                    CompanyId = x.Idempresa,
                    CompanyName = x.IdempresaNavigation.Nombre,
                }).First();

            return Ok(new { Message = "Se edito la empresa", Data = eventTypeRes, Status = 200 });
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var eventType = _db.TipoEventos.Find(id);
            if (eventType == null)
            {
                return NotFound(new { Message = "No se encontro el tipo Evento", Data = ' ', Status = 404 });
            }

            _db.TipoEventos.Remove(eventType);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino el tipo Evento", Data = ' ', Status = 200 });

        }
    }
}
