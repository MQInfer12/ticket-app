using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

namespace server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriaController : Controller
  {
    private DBContext _db;

    public CategoriaController(DBContext db)
    {
      _db = db;
    }


    [HttpGet, Authorize]
    public IActionResult Get()
    {
      var rol = User.FindFirst("RoleName").Value;
      IQueryable<CategoriaResponse> categorias;
      if (rol == "Super Administrador")
      {
        categorias = _db.Categoria.Select(c => new CategoriaResponse
        {
          Id = c.Id,
          IdEmpresa = c.Idempresa,
          NombreEmpresa = c.IdempresaNavigation.Nombre,
          Nombre = c.Nombre
        });
      }
      else
      {
        var idEmpresa = User.FindFirst("CompanyId").Value;
        categorias = _db.Categoria.Where(x => x.Idempresa == Guid.Parse(idEmpresa)).Select(c => new CategoriaResponse
        {
          Id = c.Id,
          IdEmpresa = c.Idempresa,
          NombreEmpresa = c.IdempresaNavigation.Nombre,
          Nombre = c.Nombre
        });
      }
      return Ok(new BaseResponse<IQueryable<CategoriaResponse>>
      {
        Message = "Lista de categorias obtenida correctamente",
        Data = categorias,
        Status = 200
      });
    }

    [HttpPost, Authorize]
    public IActionResult Post([FromBody] CategoriaDTO req)
    {
      var registers = new Categorium
      {
        Idempresa = req.IdEmpresa,
        Nombre = req.NombreCategoria
      };
      _db.Categoria.Add(registers);
      _db.SaveChanges();

      var registersReturn = _db.Categoria.Where(x => x.Id == registers.Id).Select(c => new CategoriaResponse
      {
        Id = c.Id,
        IdEmpresa = c.Idempresa,
        Nombre = c.Nombre,
        NombreEmpresa = c.IdempresaNavigation.Nombre
      }).First();

      return Ok(new BaseResponse<CategoriaResponse> { Message = "Se creo la categoria", Data = registersReturn, Status = 200 });
    }

    [HttpPut("{id}"), Authorize]
    public IActionResult Put(Guid id, CategoriaDTO req)
    {
      var existingCategory = _db.Categoria.Find(id);

      if (existingCategory == null)
      {
        return NotFound(new { Message = "No se encontro la categoria para editar", Data = ' ', Status = 404 });
      }

      existingCategory.Idempresa = req.IdEmpresa;
      existingCategory.Nombre = req.NombreCategoria;

      _db.Categoria.Update(existingCategory);
      _db.SaveChanges();

      var putReturn = _db.Categoria.Where(x => x.Id == existingCategory.Id).Select(c => new CategoriaResponse
      {
        Id = c.Id,
        IdEmpresa = c.Idempresa,
        Nombre = c.Nombre,
        NombreEmpresa = c.IdempresaNavigation.Nombre
      }).First();

      return Ok(new BaseResponse<CategoriaResponse>
      { Message = "Se edito la categoria", Data = putReturn, Status = 200 });
    }

    [HttpDelete("{id}"), Authorize]
    public IActionResult Delete(Guid id)
    {
      var delete = _db.Categoria.Find(id);
      if (delete == null)
      {
        return NotFound(new { Message = "No se encontro la categoria", Data = ' ', Status = 404 });
      }

      _db.Categoria.Remove(delete);
      _db.SaveChanges();
      return Ok(new { Message = "Se elimino la categoria", Data = ' ', Status = 200 });
    }
  }
}