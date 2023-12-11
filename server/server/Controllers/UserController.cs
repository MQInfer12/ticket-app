using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Helps;
using server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


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

        private UsuarioDTO AuthticateUser(UsuarioDTO user)
        {
            UsuarioDTO _user = null;
            if(user.Usuario == "admin" && user.Contrasenia == "123456")
            {
                _user = new UsuarioDTO { Usuario = "Jose" };
            }
            return _user;
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
            IActionResult res = Unauthorized();
            var user = AuthticateUser(req);
            if (user != null)
            {
                var token = GenerateToken();
                res = Ok(new { Token = token });
            }
            return res;
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterDTO req)
        {
             var existPeople = _db.Personas.Any(e => e.Ci == req.Ci);
              if (existPeople)
              {
                  return Ok(new { Message = "Ya existe este CI", Data=' ', Status=409 });
              }

              var existUser = _db.Usuarios.Any(e => e.NombreUsuario == req.Usuario);
              if (existUser)
              {
                  return Ok(new { Message = "Ya existe este nombre de usuario", Data = ' ', Status = 409 });
              }

   
            if(req.Contrasenia != req.ContraseniaRepit)
            {
                return Ok(new { Message = "Las contraseñas no coindicen", Data = ' ', Status = 409 });
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

            var salt1 = HashHelps.GenerateSalt();
            var passwordHash = HashHelps.HashPasword(req.Contrasenia, out salt1);

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
