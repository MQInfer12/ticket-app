using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

namespace server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class CuentaController : Controller
  {
    private DBContext _db;

    public CuentaController(DBContext db)
    {
      _db = db;
    }

    [HttpGet, Authorize]
    public IActionResult Get()
    {
      var rol = User.FindFirst("RoleName").Value;
      IQueryable<CuentaResponse> registers;
      if (rol == "Super Administrador")
      {
        registers = _db.Cuenta
           .Select(x => new CuentaResponse
           {
             Id = x.Id,
             IdEmpresa = x.Idempresa,
             NombreEmpresa = x.IdempresaNavigation.Nombre,
             Nombre = x.Nombre,
             Tipo = x.Tipo
           });
      }
      else
      {
        var idEmpresa = User.FindFirst("CompanyId").Value;
        registers = _db.Cuenta.Where(c => c.Idempresa == Guid.Parse(idEmpresa))
           .Select(x => new CuentaResponse
           {
             Id = x.Id,
             IdEmpresa = x.Idempresa,
             NombreEmpresa = x.IdempresaNavigation.Nombre,
             Nombre = x.Nombre,
             Tipo = x.Tipo
           });
      }
      return Ok(new BaseResponse<IQueryable<CuentaResponse>>
      { Message = "Lista de contactos obtenida correctamente", Data = registers, Status = 200 });
    }

    [HttpPost, Authorize]
    public IActionResult Post([FromBody] CuentaDTO req)
    {
      var exists = _db.Cuenta.Where(x => x.Nombre.Trim().ToLower() == req.NombreCuenta.Trim().ToLower() && x.Idempresa == req.IdEmpresa).FirstOrDefault();
      if (exists != null)
      {
        return BadRequest(new { Message = "Esa cuenta ya existe", Data = ' ', Status = 409 });
      }
      var registers = new Cuentum
      {
        Idempresa = req.IdEmpresa,
        Nombre = req.NombreCuenta,
        Tipo = req.Tipo
      };
      _db.Cuenta.Add(registers);
      _db.SaveChanges();

      var registerReturn = _db.Cuenta.Where(c => c.Id == registers.Id).Select(x => new CuentaResponse
      {
        Id = x.Id,
        IdEmpresa = x.Idempresa,
        NombreEmpresa = x.IdempresaNavigation.Nombre,
        Nombre = x.Nombre,
        Tipo = x.Tipo
      }).First();
      return Ok(new BaseResponse<CuentaResponse>
      { Message = "Se creo la cuenta", Data = registerReturn, Status = 200 });
    }

    [HttpPut("{id}"), Authorize]
    public IActionResult Put(Guid id, CuentaDTO req)
    {
      var existingCuenta = _db.Cuenta.Find(id);
      if (existingCuenta == null)
      {
        return NotFound(new { Message = "No se encontro la cuenta para editar", Data = ' ', Status = 404 });
      }
      var exists = _db.Cuenta
      .Where(x =>
      x.Id != id && 
      x.Nombre.Trim().ToLower() == req.NombreCuenta.Trim().ToLower() &&
      x.Idempresa == req.IdEmpresa).FirstOrDefault();
      if (exists != null)
      {
        return BadRequest(new { Message = "Esa cuenta ya existe", Data = ' ', Status = 409 });
      }

      existingCuenta.Idempresa = req.IdEmpresa;
      existingCuenta.Nombre = req.NombreCuenta;
      existingCuenta.Tipo = req.Tipo;

      _db.Cuenta.Update(existingCuenta);
      _db.SaveChanges();

      var putReturn = _db.Cuenta.Where(x => x.Id == existingCuenta.Id).Select(c => new CuentaResponse
      {
        Id = c.Id,
        IdEmpresa = c.Idempresa,
        NombreEmpresa = c.IdempresaNavigation.Nombre,
        Nombre = c.Nombre,
        Tipo = c.Tipo
      }).First();
      return Ok(new BaseResponse<CuentaResponse>
      { Message = "Se edito la cuenta", Data = putReturn, Status = 200 });
    }

    [HttpDelete("{id}"), Authorize]
    public IActionResult Delete(string id)
    {
      var delete = _db.Cuenta.Find(id);
      if (delete == null)
      {
        return NotFound(new { Message = "No se encontro la cuenta", Data = ' ', Status = 404 });
      }

      _db.Cuenta.Remove(delete);
      _db.SaveChanges();
      return Ok(new { Message = "Se elimino la cuenta", Data = ' ', Status = 200 });
    }
  }
}