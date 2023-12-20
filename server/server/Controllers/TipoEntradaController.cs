using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Model;

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
        [Route("GetTipoEntradaById{id}")]
        public IActionResult GetTipoEntradaById(Guid id)
        {
            var entrada = _db.TipoEntrada.Where(e => e.Idtipoevento == id).ToList();
            return Ok(new { Message = "Lista de entradas obtenida correctamente", Data = entrada, Status = 200 });
        }
        [HttpPost, Authorize]
        [Route("PostTipoEntrada")]
        public IActionResult PostTipoEntada(TipoEntradaDTO TipoEntradum)
        {
            var e = new TipoEntradum
            {
                Nombre = TipoEntradum.NombreEvent,
                Costo = TipoEntradum.CostoEvent,
                Cantidadinicial = TipoEntradum.CantidadinicialEvent,
                Stock = TipoEntradum.StockEvent
            };
            _db.TipoEntrada.Add(e);
            _db.SaveChanges();
            return Ok(new { Message = "Se añadio la entrada", Data = e, Status = 200 });
        }
        [HttpPut("{id}"), Authorize]
        public IActionResult PutTipoEntradum(Guid id, TipoEntradaDTO TipoEntradum)
        {
            var e = _db.TipoEntrada.Find(id);
            if (e == null)
            {
                return NotFound(new { Message = "No se encontro la entrada", Data = ' ', Status = 404 });
            }
            e.Nombre = TipoEntradum.NombreEvent;
            e.Costo = TipoEntradum.CostoEvent;
            e.Cantidadinicial = TipoEntradum.CantidadinicialEvent;
            e.Stock = TipoEntradum.StockEvent;
            _db.SaveChanges();
            return Ok(new { Message = "Se edito la entrada", Data = e, Status = 200 });
        }
        [HttpDelete("{id}"), Authorize]
        public IActionResult DeleteTipoEntradum (Guid id)
        {
            var e = _db.TipoEntrada.Find (id);
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
