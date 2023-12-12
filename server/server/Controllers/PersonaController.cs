using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Model;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        //variable global
        private DBContext _db;
        //constructor inica lo primero del sistema
        public PersonaController(DBContext db) {
        _db = db;
        }
        [HttpGet, Authorize]  // hacer peticion get // 

        public IActionResult Get()
        {
            var persona = _db.Personas.Join(_db.Usuarios,
                person => person.Id,
                user => user.Idpersona,
                (person, user) => new
                {
                    IdPersona = person.Id,
                    Ci = person.Ci,
                    Nombres = person.Nombres,
                    Appaterno = person.Appaterno,
                    Apmaterno = person.Apmaterno,
                    Usuario = user.NombreUsuario
                }
                );
            //primero se pone la tabla con la que se esta trabajando y despues con la que la relacionas//
            return Ok(new { Message = "Lista de Personas", Data = persona, Status = 200 });
        }
        [HttpPost, Authorize] //    Tipo post //
        public IActionResult Post([FromBody] PersonaDTO req)
        {
            var people = new Persona
            {
                Ci = req.Ci,
                Nombres = req.Nombres,
                Appaterno = req.Appaterno,
                Apmaterno = req.Apmaterno
            };_db.Personas.Add(people);
            _db.SaveChanges();
            var user = new Usuario
            {
                Idpersona = people.Id,
                NombreUsuario = req.NombreUsurio,
                Contrasenia = req.Password
            };_db.Usuarios.Add(user);
            _db.SaveChanges();
            return Ok(new { Message = "Se agrego usuario", Data = people, Status = 201 });
        }
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {
            var person = _db.Personas.Find(id);
            _db.Personas.Remove(person);
            _db.SaveChanges();
            var user = _db.Usuarios.FirstOrDefault(v => v.Idpersona == id);
            _db.Usuarios.Remove(user);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la persona", Data = ' ', Status = 200 });
        }     
    }
}
