using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoRolController : ControllerBase
    {
        private DBContext _db;
        public TipoRolController(DBContext db)
        {
            _db = db;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var rolsType = _db.TipoRols;
            return Ok(new { Message = "Datos obtenidos con exito", Data = rolsType, Status = 200 });
        }

        [HttpGet("GetRolesByPersona/{id}"), Authorize]
        public IActionResult GetRolsByUser(Guid id)
        {
            var rol = User.FindFirst("RoleName").Value;
            var idEmpresa = User.FindFirst("CompanyId").Value;
            if(rol == "Super Administrador") {
                var userRols = _db.RolUsuarios.Where(ru => ru.IdusuarioNavigation.Idpersona == id).Select(
                    x => new
                    {
                        id = x.Id,
                        idRol = x.Idtiporol,
                        rol = x.IdtiporolNavigation.Nombre,
                        idEmpresa = x.Idempresa,
                        empresa = x.IdempresaNavigation.Nombre,
                        estado = x.Estado,
                        idUsuario = x.Idusuario,
                        nombreUsuario = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                        apellidoPaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                        apellidoMaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno
                    }
                    ).ToList();

                return Ok(new { Message = "Datos obtenidos con exito", Data = userRols, Status = 200 });
            } else {
                var userRols = _db.RolUsuarios.Where(ru => ru.Idempresa == Guid.Parse(idEmpresa) && ru.IdusuarioNavigation.Idpersona == id).Select(
                    x => new
                    {
                        id = x.Id,
                        idRol = x.Idtiporol,
                        rol = x.IdtiporolNavigation.Nombre,
                        idEmpresa = x.Idempresa,
                        empresa = x.IdempresaNavigation.Nombre,
                        estado = x.Estado,
                        idUsuario = x.Idusuario,
                        nombreUsuario = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                        apellidoPaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                        apellidoMaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno
                    }
                    ).ToList();

                return Ok(new { Message = "Datos obtenidos con exito", Data = userRols, Status = 200 });
            }
        }

        [HttpGet("GetRolesByEmpresa/{id}"), Authorize]
        public IActionResult GetRolesByEmpresa(Guid id)
        {
            var userRols = _db.RolUsuarios.Where(ru => ru.Idempresa == id).Select(
                x => new
                {
                    id = x.Id,
                    idRol = x.Idtiporol,
                    rol = x.IdtiporolNavigation.Nombre,
                    idEmpresa = x.Idempresa,
                    empresa = x.IdempresaNavigation.Nombre,
                    estado = x.Estado,
                    idUsuario = x.Idusuario,
                    nombreUsuario = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                    apellidoPaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                    apellidoMaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno
                }
                ).ToList();
            if (userRols.Count == 0)
            {
                return NotFound(new { Message = "Esta empresa no tiene usuarios", Data = ' ', Status = 404 });
            }

            return Ok(new { Message = "Datos obtenidos con exito", Data = userRols, Status = 200 });
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] TipoRolDTO req)
        {
            var rolType = new TipoRol
            {
                Nombre = req.Nombre,
            };

            _db.TipoRols.Add(rolType);
            _db.SaveChanges();

            return Ok(new { Message = "Se creo el Rol", Data = rolType, Status = 201 });
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {
            var rolType = _db.TipoRols.Find(id);

            _db.TipoRols.Remove(rolType);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino el Rol", Data = ' ', Status = 200 });
        }

    }
}
