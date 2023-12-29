using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Model;

namespace server.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private IConfiguration _config;
        private DBContext _db;

        public ItemController(IConfiguration config, DBContext db)
        {
            _config = config;
            _db = db;
        }
        [HttpGet, Authorize]
        [Route("GetItemById/{id}")]
        public IActionResult GetItemById(Guid id)
        {
            var item = _db.Items.Where(i => i.Idcategoria == id).ToList();
            return Ok(new { Message = "Item Obtenido correctamente", Data = item, Status = 200 });
        }
        [HttpPost, Authorize]
        [Route("PostItem")]
        public IActionResult PostItem(ItemDTO Item)
        {
            var i = new Item
            {
                Idcategoria = Item.IdCategoria,
                Detalle = Item.DetalleItem,
                Fecharegistro = Item.FechaRegistroItem,
                Cantidadinicial = Item.CantidadinicialItem,
                Stock = Item.StockItem,
                Costo = Item.CostoItem,
            };
            _db.Items.Add(i);
            _db.SaveChanges();
            return Ok(new { Message = "Se añadio el contacto", Data = i, Status = 200 });
        }
        [HttpPut ("id"), Authorize]
        public IActionResult PutItem(Guid id, ItemDTO Item)
        {
            var i = _db.Items.Find(id);
            if (i == null)
            {
                return NotFound(new { Message = "No se encontro el item", Data = ' ', Status = 404 });
            }
            i.Idcategoria = Item.IdCategoria;
            i.Detalle = Item.DetalleItem;
            i.Fecharegistro = Item.FechaRegistroItem;
            i.Cantidadinicial = Item.CantidadinicialItem;
            i.Stock = Item.StockItem;
            i.Costo = Item.CostoItem;
            _db.SaveChanges();
            return Ok(new { Message = "Se edito el item correctamente", Data = i, Status = 200 });
        }
        [HttpDelete ("id"), Authorize]
        public IActionResult DeleteItem(Guid id)
        {
            var i = _db.Items.Find(id);
            if (i == null)
            {
                return NotFound(new { Message = "No se encontro el item", Data = i, Status = 404 });
            }
            _db.Items.Remove(i);
            _db.SaveChanges();

            return Ok(new { Message = "Se elimino el item", Data = i, Status = 200 });
        }
    }
}
