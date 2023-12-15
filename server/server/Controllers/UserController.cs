using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Helps;
using server.Model;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private DBContext _db;

        public UserController(IConfiguration configuration, DBContext db)
        {
            _config = configuration;
            _db = db;

        }

        private string GenerateToken(Usuario user, string nameRole, Guid roleId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>(){
                new Claim("UserId", user.Id.ToString()),
                new Claim("RoleName", nameRole.ToString()),
                new Claim("RoleId", roleId.ToString()),
            };
      
            //Add ROLE
            claims.Add(new Claim(ClaimTypes.Role, nameRole.ToString()));

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet, Authorize]
        [Route("GetUserByToken")]
        public IActionResult GetUserToken()
        {

            string userId = User.FindFirst("UserId").Value; //get id

            var userRes = _db.Usuarios.Where(u => u.Id == Guid.Parse(userId))
                .Join(_db.Personas,
                user => user.Idpersona,
                person => person.Id,
                (user, person) => new
                {
                    UserId = user.Id,
                    NombreUsuario = user.NombreUsuario,
                    Nombres = person.Nombres,
                    Ci = person.Ci,
                    ApPaterno = person.Appaterno,
                    ApMaterno = person.Apmaterno,
                }
                ).First();

            return Ok(new { Message = "Token obtenido", Data = userRes, Status = 200 });
        }

        [HttpGet, Authorize]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById(Guid id)
        {


            var userRes = _db.RolUsuarios.Where(v => v.Idusuario == id)
                .Join(_db.TipoRols,
                userRole => userRole.Idtiporol,
                roleType => roleType.Id,
                (userRole, roleType) => new
                {
                    UserId = userRole.Idusuario, 
                    RoleTypeId = userRole.Idtiporol,
                    CompanyId = userRole.Idempresa,
                    RoleName = roleType.Nombre
                }).Join(_db.Usuarios,
                userRole => userRole.UserId,
                user => user.Id,
                (userRole, user) => new
                {
                    UserId = userRole.UserId,
                    RoleTypeId = userRole.RoleTypeId,
                    CompanyId = userRole.CompanyId,
                    RoleName = userRole.RoleName,
                    UserName = user.NombreUsuario,
                    Password = user.Contrasenia
                }
                ).Join(_db.Empresas,
                 userRole => userRole.CompanyId,
                 company => company.Id,
                 (userRole, company) => new
                 {
                     UserId = userRole.UserId,
                     RoleTypeId = userRole.RoleTypeId,
                     CompanyId = userRole.CompanyId,
                     RoleName = userRole.RoleName,
                     UserName = userRole.UserName,
                     Password = userRole.Password,
                     CompanyName = company.Nombre,
                     CompanyAddress = company.Direccion,
                     CompanyState = company.Estado,
                 }
                 )
                .First();

            return Ok(new { Message = "Datos obtenidos", Data = userRes, Status = 200 });
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UsuarioDTO req)
        {
            var user = _db.Usuarios.FirstOrDefault(e => e.NombreUsuario == req.Usuario);
            if (user != null)
            {
                //Verify password

                var passwordDecrypt = HashHelps.Decrypt(user.Contrasenia);

                if (passwordDecrypt == req.Contrasenia)
                {

                    //get the id RoleType from the table UserRol
                    var userRol = _db.RolUsuarios.FirstOrDefault(v => v.Idusuario == user.Id);

                    if(userRol == null)
                    {
                        return NotFound(new { Message = "No se encontro el rol de ese usuario", Data = ' ', Status = 404 });
                    }

                    //get the name of the rol iof the table roleType
                    var roletype = _db.TipoRols.FirstOrDefault(v => v.Id == userRol.Idtiporol);

                    if (roletype == null)
                    {
                        return NotFound(new { Message = "No se encontro el nombre de ese rol", Data = ' ', Status = 404 });
                    }


                    var token = GenerateToken(user, roletype.Nombre, roletype.Id);
                    return Ok(new { Message = "Bienvenido", Data = token, Status = 200 });
                }
                return BadRequest(new { Message = "Contraseña incorrecta", Data = ' ', Status = 409 });

            }
            return NotFound(new { Message = "No se encontro el usuario", Data = ' ', Status = 404 });
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterDTO req)
        {
            // ====================== SAVE IN PEOPLE TABLE ======================
            var existPeople = _db.Personas.Any(e => e.Ci == req.Ci);
            if (existPeople)
            {
                return BadRequest(new { Message = "Ya existe este CI", Data = ' ', Status = 409 });
            }

            var existUser = _db.Usuarios.Any(e => e.NombreUsuario == req.Usuario);
            if (existUser)
            {
                return BadRequest(new { Message = "Ya existe este nombre de usuario", Data = ' ', Status = 409 });
            }


            if (req.Contrasenia != req.ContraseniaRepit)
            {
                return BadRequest(new { Message = "Las contraseñas no coindicen", Data = ' ', Status = 409 });
            }

            //get role type
            var roleType = _db.TipoRols.FirstOrDefault((v) => v.Nombre == "Cliente");
            if(roleType == null)
            {
                return NotFound(new { Message = "No se encontro el rol Cliente", Data = ' ', Status = 404 });
            }

            //get company
            var company = _db.Empresas.FirstOrDefault(v => v.Nombre == "CBBA");
            if (company == null)
            {
                return NotFound(new { Message = "No se encontro la empresa global 'CBBA'", Data = ' ', Status = 404 });
            }

            var people = new Persona
            {
                Ci = req.Ci,
                Apmaterno = req.Apmaterno,
                Appaterno = req.Appaterno,
                Nombres = req.Nombres,
            };

            _db.Personas.Add(people);
            _db.SaveChanges();


            // ====================== SAVE IN USER TABLE ======================
            //Generate password

            var passwordHash = HashHelps.Encrypt(req.Contrasenia);

            var user = new Usuario
            {
                NombreUsuario = req.Usuario,
                Contrasenia = passwordHash,
                Idpersona = people.Id,
            };
            _db.Usuarios.Add(user);
            _db.SaveChanges();


            // ====================== SAVE IN USERROLE TABLE ======================

            var userRole = new RolUsuario
            {
                Idusuario = user.Id,
                Idtiporol = roleType.Id,
                Idempresa = company.Id,
                Estado = true
            };
            _db.RolUsuarios.Add(userRole);
            _db.SaveChanges();

            var token = GenerateToken(user, roleType.Nombre, roleType.Id);

            return Ok(new { Message = "Se registro su cuenta con exito", Data = token, Status = 200 });
        }
    }
}
