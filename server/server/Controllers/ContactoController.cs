using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Model;

namespace server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : Controller
    {

        private IConfiguration _config;
        private DBContext _db;

        public ContactoController(IConfiguration config, DBContext db)
        {
            _config = config;
            _db = db;
        }
        [HttpGet, Authorize]
        [Route ("GetContactoById/{id}")]
        public IActionResult GetContactoById(Guid id)
        {
            var contact = _db.Contactos.Where(c => c.Personaempresa == id).ToList();  // c (puede ser cualqueir letra) pero define una variable y decimos cual es la acion que realizara c agarra de empersa persona y comparara su id para que  ToList muestre todos los datos de ese id (ToList muestra varios datos)
            return Ok(new { Message = "Lista de contactos obtenida correctamente", Data = contact, Status = 200 });
        }
        [HttpPost, Authorize]
        [Route ("PostContacto")]
        public IActionResult PostContacto(ContactoDTO Contacto)
        {
            var c = new Contacto
            {
                ContactoName = Contacto.Contacto,
                Tipo = Contacto.TipoContacto,
                Personaempresa = Contacto.Personaempresa
            };
            _db.Contactos.Add(c);
            _db.SaveChanges();
            return Ok(new { Message = "Se añadio el contacto", Data = c, Status = 200 });
        }
        [HttpPut ("{id}"), Authorize]
        public IActionResult PutContacto(Guid id, ContactoDTO  Contacto)
        {
            var c = _db.Contactos.Find(id);
            if (c == null)
            {
                return NotFound(new { Message = "No se encontro el contacto", Data = ' ', Status = 404 });
            }
            c.ContactoName = Contacto.Contacto;
            c.Tipo = Contacto.TipoContacto;
            c.Personaempresa = Contacto.Personaempresa;
            _db.SaveChanges ();

            return Ok(new { Message = "Se edito el contacto", Data = c, Status = 200 });
        }
        [HttpDelete("{id}"), Authorize]
        public IActionResult DeleteContacto(Guid id) 
        {
            var c = _db.Contactos.Find(id);
            if (c == null)
            {
                return NotFound(new { Message = "No se encontro el contacto", Data = ' ', Status = 404 });
            }
            _db.Contactos.Remove(c);
            _db.SaveChanges();

            return Ok(new { Message = "Se elimino el contacto", Data = c, Status = 200 });
        }

    }
}
