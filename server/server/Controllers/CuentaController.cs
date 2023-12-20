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
  }
}