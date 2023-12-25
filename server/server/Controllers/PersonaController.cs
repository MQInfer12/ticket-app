using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Helps;
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
        public PersonaController(DBContext db)
        {
            _db = db;
        }
        [HttpGet, Authorize]  // hacer peticion get // 

        public IActionResult Get()
        {
            var rol = User.FindFirst("RoleName").Value;
            if (rol == "Super Administrador")
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
                return Ok(new { Message = "Lista de Personas", Data = persona, Status = 200 });
            }
            else
            {
                var idEmpresa = User.FindFirst("CompanyId").Value;
                var persona = _db.RolUsuarios.Where(ru => ru.Idempresa == Guid.Parse(idEmpresa)).Select(ru => new
                {
                    IdPersona = ru.IdusuarioNavigation.Idpersona,
                    Ci = ru.IdusuarioNavigation.IdpersonaNavigation.Ci,
                    Nombres = ru.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                    Appaterno = ru.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                    Apmaterno = ru.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                    Usuario = ru.IdusuarioNavigation.NombreUsuario
                }).ToList();

                //remove equal elements
                persona = persona.Distinct().ToList(); 

                return Ok(new { Message = "Lista de Personas", Data = persona, Status = 200 });
            }
        }

        [HttpGet("EmpresasRols"), Authorize]
        public IActionResult GetRolsEmpresas()
        {
            var rols = _db.TipoRols.ToList();
            var empresas = _db.Empresas.ToList();
            return Ok(new { Message = "Datos obtenidos con exito", Data = new { rols, empresas }, Status = 200 });
        }

        [HttpPost, Authorize] //    Tipo post //
        public IActionResult Post([FromBody] PersonaDTO req)
        {
            var exists = _db.Usuarios.Any(e => e.NombreUsuario == req.NombreUsurio);
            if (exists)
            {
                return BadRequest(new { Message = "Ya existe este usuario", Data = ' ', Status = 400 });
            }
            var existsCi = _db.Personas.Any(e => e.Ci == req.Ci);
            if (existsCi)
            {
                return BadRequest(new { Message = "Ya existe este CI", Data = ' ', Status = 400 });
            }


            var user = new Usuario
            {
                NombreUsuario = req.NombreUsurio,
                Contrasenia = HashHelper.Encrypt(req.Password),
                IdpersonaNavigation = new Persona
                {
                    Ci = req.Ci,
                    Nombres = req.Nombres,
                    Appaterno = req.Appaterno,
                    Apmaterno = req.Apmaterno
                }
            };

            _db.Usuarios.Add(user);

            _db.SaveChanges();

            var rolUser = new RolUsuario
            {
                Idtiporol = req.IdTipoRol,
                Idempresa = req.IdEmpresa,
                Idusuario = user.Id
            };

            _db.RolUsuarios.Add(rolUser);

            _db.SaveChanges();

            var userGet = _db.Usuarios.Where(v => v.Id == user.Id).Join(_db.Personas,

                user => user.Idpersona,
                person => person.Id,
                (user, person) => new
                {
                    IdPersona = person.Id,
                    Ci = person.Ci,
                    Nombres = person.Nombres,
                    Appaterno = person.Appaterno,
                    Apmaterno = person.Apmaterno,
                    Usuario = user.NombreUsuario
                }
                ).First();

            return Ok(new { Message = "Se agrego usuario", Data = userGet, Status = 201 });
        }


        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, PersonaDTO req)
        {

            var existPeoples = _db.Personas.Find(id);

            if (existPeoples == null)
            {
                return NotFound(new { Message = "No se encontro la persona", Data = ' ', Status = 404 });
            }

            var existUser = _db.Usuarios.FirstOrDefault(v => v.Idpersona == existPeoples.Id);

            if (existUser == null)
            {
                return NotFound(new { Message = "No se encontro el usuario", Data = ' ', Status = 404 });
            }

            var exists = _db.Usuarios.Any(e => e.NombreUsuario == req.NombreUsurio && e.Id != existUser.Id);
            if (exists)
            {
                return BadRequest(new { Message = "Ya existe este usuario", Data = ' ', Status = 400 });
            }
            var existsCi = _db.Personas.Any(e => e.Ci == req.Ci && e.Id != id);
            if (existsCi)
            {
                return BadRequest(new { Message = "Ya existe este CI", Data = ' ', Status = 400 });
            }


            existPeoples.Nombres = req.Nombres;
            existPeoples.Appaterno = req.Appaterno;
            existPeoples.Apmaterno = req.Apmaterno;
            existPeoples.Ci = req.Ci;

            _db.Personas.Update(existPeoples);


            existUser.NombreUsuario = req.NombreUsurio;

            _db.Usuarios.Update(existUser);

            _db.SaveChanges();

            var userGet = _db.Usuarios.Where(v => v.Id == existUser.Id).Join(_db.Personas,

             user => user.Idpersona,
             person => person.Id,
             (user, person) => new
             {
                 IdPersona = person.Id,
                 Ci = person.Ci,
                 Nombres = person.Nombres,
                 Appaterno = person.Appaterno,
                 Apmaterno = person.Apmaterno,
                 Usuario = user.NombreUsuario
             }
             ).First();
            return Ok(new { Message = "Se edito la persona", Data = userGet, Status = 200 });
        }

        [HttpGet("GetPersonaPage/{id}"), Authorize]
        public IActionResult GetPersonaPage(Guid id)
        {
            var userData = _db.Usuarios.Where(u => u.Idpersona == id).Select(x => new
            {
                fullName = x.IdpersonaNavigation.Nombres + " " + x.IdpersonaNavigation.Appaterno + " " + x.IdpersonaNavigation.Apmaterno,
                ci = x.IdpersonaNavigation.Ci,
                idUsuario = x.Id,
                usuario = x.NombreUsuario
            }).FirstOrDefault();
            var rols = _db.TipoRols.ToList();
            var empresas = _db.Empresas.ToList();
            if (userData == null)
            {
                return NotFound(new { Message = "No se encontro este usuario", Data = ' ', Status = 404 });
            }

            return Ok(new { Message = "Datos obtenidos con exito", Data = new { userData, rols, empresas }, Status = 200 });
        }


        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {
            var persona = _db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound(new { Message = "No se encontro la persona", Data = ' ', Status = 404 });
            }

            var user = _db.Usuarios.FirstOrDefault(v => v.Idpersona == id);
            _db.Usuarios.Remove(user);

            var person = _db.Personas.Find(id);
            _db.Personas.Remove(person);

            _db.SaveChanges();

            return Ok(new { Message = "Se elimino la persona", Data = ' ', Status = 200 });
        }
    }
}
