using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController(IConfiguration configuration)
        {
            _config = configuration;
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

        private string GenerateToken(UsuarioDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public IActionResult Login(UsuarioDTO req)
        {
            IActionResult res = Unauthorized();
            var user = AuthticateUser(req);
            if (user != null)
            {
                var token = GenerateToken(user);
                res = Ok(new { Token = token });
            }
            return res;
        }
    }
}
