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

        [HttpGet("GetRolesByUser/{id}"), Authorize]
        public IActionResult GetRolsByUser(Guid id)
        {
            //var userrols = _db.RolUsuarios.Where(v=> v.Idusuario == id);
            var query= from rolUsuario in _db.RolUsuarios
                          join tipoRol in _db.TipoRols 
                          on rolUsuario.Idtiporol equals tipoRol.Id
                          where rolUsuario.Idusuario == id
                       select tipoRol.Nombre;

            var userRols = query.ToList();
            if (userRols == null)
            {
                return NotFound(new { Message = "Este usuario no tiene ningun rol", Data = ' ', Status = 404 });
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
