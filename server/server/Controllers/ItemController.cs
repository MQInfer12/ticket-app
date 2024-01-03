using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model;
using server.Responses;

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
        [Route("GetItemByCategory/{id}")]
        public IActionResult GetItemByCategory(Guid id)
        {
            var item = _db.Items.Where(i => i.Idcategoria == id).Select(x => new ItemResponse
            {
                Id = x.Id,
                IdCategoria = id,
                NombreCategoria = x.IdcategoriaNavigation.Nombre,
                NombreEmpresa = x.IdcategoriaNavigation.IdempresaNavigation.Nombre,
                Detalle = x.Detalle,
                FechaRegistro = x.Fecharegistro,
                CantidadInicial = x.Cantidadinicial,
                Stock = x.Stock,
                Costo = x.Costo
            });
            return Ok(new BaseResponse<IQueryable<ItemResponse>>
            {
                Message = "Datos obtenidos con exito",
                Data = item,
                Status = 200
            });
        }
        [HttpPost, Authorize]
        [Route("PostItem")]
        public IActionResult PostItem(ItemDTO Item)
        {
            var category = _db.Categoria.Include(x => x.IdempresaNavigation).Where(i => i.Id == Item.IdCategoria).First();
            if (category == null)
            {
                return NotFound(new { Message = "No se encontro la categoria", Data = ' ', Status = 404 });
            }
            var i = new Item
            {
                IdcategoriaNavigation = category,
                Detalle = Item.DetalleItem,
                Fecharegistro = Item.FechaRegistroItem,
                Cantidadinicial = Item.CantidadinicialItem,
                Stock = Item.StockItem,
                Costo = Item.CostoItem,
            };
            _db.Items.Add(i);
            _db.SaveChanges();
            var itemResp = new ItemResponse
            {
                Id = i.Id,
                IdCategoria = i.Idcategoria,
                NombreCategoria = i.IdcategoriaNavigation.Nombre,
                NombreEmpresa = i.IdcategoriaNavigation.IdempresaNavigation.Nombre,
                Detalle = i.Detalle,
                FechaRegistro = i.Fecharegistro,
                CantidadInicial = i.Cantidadinicial,
                Stock = i.Stock,
                Costo = i.Costo
            };
            return Ok(new { Message = "Se añadio el contacto", Data = itemResp, Status = 200 });
        }
        [HttpPut("{id}"), Authorize]
        public IActionResult PutItem(Guid id, ItemDTO Item)
        {
            var category = _db.Categoria.Include(x => x.IdempresaNavigation).Where(i => i.Id == Item.IdCategoria).First();
            if (category == null)
            {
                return NotFound(new { Message = "No se encontro la categoria", Data = ' ', Status = 404 });
            }
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
            var itemResp = new ItemResponse
            {
                Id = i.Id,
                IdCategoria = i.Idcategoria,
                NombreCategoria = i.IdcategoriaNavigation.Nombre,
                NombreEmpresa = i.IdcategoriaNavigation.IdempresaNavigation.Nombre,
                Detalle = i.Detalle,
                FechaRegistro = i.Fecharegistro,
                CantidadInicial = i.Cantidadinicial,
                Stock = i.Stock,
                Costo = i.Costo
            };
            return Ok(new { Message = "Se edito el item correctamente", Data = itemResp, Status = 200 });
        }
        [HttpDelete("{id}"), Authorize]
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
