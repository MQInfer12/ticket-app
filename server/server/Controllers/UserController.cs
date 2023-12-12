using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Helps;
using server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
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

      private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
             
                    var token = GenerateToken();
                    return Ok(new { Token = token, Data = user });
                }
                return BadRequest(new { Message = "Contraseña incorrecta", Data = ' ', Status = 409 });

            }
            return NotFound(new { Message = "No se encontro el usuario", Data = ' ', Status = 404 });
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterDTO req)
        {
            var existPeople = _db.Personas.Any(e => e.Ci == req.Ci);
              if (existPeople)
              {
                  return BadRequest(new { Message = "Ya existe este CI", Data=' ', Status=409 });
              }

              var existUser = _db.Usuarios.Any(e => e.NombreUsuario == req.Usuario);
              if (existUser)
              {
                  return BadRequest(new { Message = "Ya existe este nombre de usuario", Data = ' ', Status = 409 });
              }

   
            if(req.Contrasenia != req.ContraseniaRepit)
            {
                return BadRequest(new { Message = "Las contraseñas no coindicen", Data = ' ', Status = 409 });
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


            var token = GenerateToken();

            return Ok(new { Message = "Se registro su cuenta con exito", Data = token, Status= 200 });
        }
    }
}
