using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Model;
using server.Responses;

namespace server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TipoEntradaController : Controller
    {

        private IConfiguration _config;
        private DBContext _db;

        public TipoEntradaController(IConfiguration config, DBContext db)
        {
            _config = config;
            _db = db;
        }

        [HttpGet, Authorize]
        [Route("GetTipoEntrada")]
        public IActionResult GetTipoEntrada()
        {
            var rol = User.FindFirst("RoleName").Value;
            IQueryable<EntradaResponse> entradasType;
            if (rol == "Super Administrador")
            {
                entradasType = _db.TipoEntrada.Select(x => new EntradaResponse
                {
                    Id = x.Id,
                    IdTipoEvento = x.Idtipoevento,
                    NombreEvento = x.IdtipoeventoNavigation.Nombre,
                    NombreEmpresa = x.IdtipoeventoNavigation.IdempresaNavigation.Nombre,
                    Nombre = x.Nombre,
                    Costo = x.Costo,
                    CantidadInicial = x.Cantidadinicial,
                    Stock = x.Stock
                });
            }
            else
            {
                var idEmpresa = User.FindFirst("CompanyId").Value;
                entradasType = _db.TipoEntrada.Where(e => e.IdtipoeventoNavigation.Idempresa == Guid.Parse(idEmpresa)).Select(x => new EntradaResponse
                {
                    Id = x.Id,
                    IdTipoEvento = x.Idtipoevento,
                    NombreEvento = x.IdtipoeventoNavigation.Nombre,
                    NombreEmpresa = x.IdtipoeventoNavigation.IdempresaNavigation.Nombre,
                    Nombre = x.Nombre,
                    Costo = x.Costo,
                    CantidadInicial = x.Cantidadinicial,
                    Stock = x.Stock
                });
            }
            if (entradasType == null)
            {
                return Ok(new BaseResponse<string> { Message = "No se encontraron datos", Data = " ", Status = 404 });
            }
            return Ok(new BaseResponse<IQueryable<EntradaResponse>>
            { Message = "Lista de entradas obtenida correctamente", Data = entradasType, Status = 200 });
        }

        [HttpGet, Authorize]
        [Route("GetTipoEntradaByEvento/{id}")]
        public IActionResult GetTipoEntradaById(Guid id)
        {
            var entrada = _db.TipoEntrada.Where(e => e.Idtipoevento == id).Select(x => new EntradaResponse
            {
                Id = x.Id,
                IdTipoEvento = x.Idtipoevento,
                NombreEvento = x.IdtipoeventoNavigation.Nombre,
                NombreEmpresa = x.IdtipoeventoNavigation.IdempresaNavigation.Nombre,
                Nombre = x.Nombre,
                Costo = x.Costo,
                CantidadInicial = x.Cantidadinicial,
                Stock = x.Stock
            });
            return Ok(new BaseResponse<IQueryable<EntradaResponse>>
            { Message = "Lista de entradas obtenida correctamente", Data = entrada, Status = 200 });
        }

        [HttpPost, Authorize]
        [Route("PostTipoEntrada")]
        public IActionResult PostTipoEntada(TipoEntradaDTO TipoEntradum)
        {
            var e = new TipoEntradum
            {
                Idtipoevento = TipoEntradum.idEvent,
                Nombre = TipoEntradum.NombreEvent,
                Costo = TipoEntradum.CostoEvent,
                Cantidadinicial = TipoEntradum.CantidadinicialEvent,
                Stock = TipoEntradum.StockEvent
            };

            var evento = _db.TipoEventos.Find(TipoEntradum.idEvent);
            var allTicketQuantity = _db.TipoEntrada.Where(te => te.Idtipoevento == TipoEntradum.idEvent).Sum(te => te.Cantidadinicial);
            if (allTicketQuantity + TipoEntradum.CantidadinicialEvent > evento.Cantidad)
            {
                var restantes = evento.Cantidad - allTicketQuantity;
                return BadRequest(new { Message = "La cantidad inicial máxima excede la del evento (disponibles " + restantes.ToString() + ")", Data = ' ', Status = 409 });
            }

            _db.TipoEntrada.Add(e);
            _db.SaveChanges();
            var entradaResponse = _db.TipoEntrada.Where(x => x.Id == e.Id).Select(x => new EntradaResponse
            {
                Id = x.Id,
                IdTipoEvento = x.Idtipoevento,
                NombreEvento = x.IdtipoeventoNavigation.Nombre,
                NombreEmpresa = x.IdtipoeventoNavigation.IdempresaNavigation.Nombre,
                Nombre = x.Nombre,
                Costo = x.Costo,
                CantidadInicial = x.Cantidadinicial,
                Stock = x.Stock
            }).First();
            return Ok(new { Message = "Se añadio la entrada", Data = entradaResponse, Status = 200 });
        }
        [HttpPut("{id}"), Authorize]
        public IActionResult PutTipoEntradum(Guid id, TipoEntradaDTO TipoEntradum)
        {
            if (TipoEntradum.StockEvent > TipoEntradum.CantidadinicialEvent)
            {
                return BadRequest(new { Message = "El stock no puede superar a la cantidad inicial", Data = ' ', Status = 409 });
            }

            var e = _db.TipoEntrada.Find(id);
            if (e == null)
            {
                return NotFound(new { Message = "No se encontro la entrada", Data = ' ', Status = 404 });
            }

            var evento = _db.TipoEventos.Find(TipoEntradum.idEvent);
            var allTicketQuantity = _db.TipoEntrada.Where(te => te.Idtipoevento == TipoEntradum.idEvent).Sum(te => te.Cantidadinicial);
            if (allTicketQuantity + TipoEntradum.CantidadinicialEvent - e.Cantidadinicial > evento.Cantidad)
            {
                var restantes = evento.Cantidad - allTicketQuantity + e.Cantidadinicial;
                return BadRequest(new { Message = "La cantidad inicial máxima excede la del evento (disponibles " + restantes.ToString() + ")", Data = ' ', Status = 409 });
            }

            e.Nombre = TipoEntradum.NombreEvent;
            e.Costo = TipoEntradum.CostoEvent;
            e.Cantidadinicial = TipoEntradum.CantidadinicialEvent;
            e.Stock = TipoEntradum.StockEvent;
            _db.SaveChanges();
            var entradaResponse = _db.TipoEntrada.Where(x => x.Id == e.Id).Select(x => new EntradaResponse
            {
                Id = x.Id,
                IdTipoEvento = x.Idtipoevento,
                NombreEvento = x.IdtipoeventoNavigation.Nombre,
                NombreEmpresa = x.IdtipoeventoNavigation.IdempresaNavigation.Nombre,
                Nombre = x.Nombre,
                Costo = x.Costo,
                CantidadInicial = x.Cantidadinicial,
                Stock = x.Stock
            }).First();
            return Ok(new { Message = "Se edito la entrada", Data = entradaResponse, Status = 200 });
        }
        [HttpDelete("{id}"), Authorize]
        public IActionResult DeleteTipoEntradum(Guid id)
        {
            var e = _db.TipoEntrada.Find(id);
            if (e == null)
            {
                return NotFound(new { Message = "No se encontro la entrada", Data = e, Status = 404 });
            }
            _db.TipoEntrada.Remove(e);
            _db.SaveChanges();

            return Ok(new { Message = "Se elimino la entrada", Data = e, Status = 200 });
        }
    }
}
