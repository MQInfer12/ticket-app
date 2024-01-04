using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;



namespace server.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _config;

        public TokenHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(RolUsuario userRol, string nameRole, Guid roleId, Guid? companyId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>(){
                new Claim("UserRolId", userRol.Id.ToString()),
                new Claim("RoleName", nameRole.ToString()),
                new Claim("RoleId", roleId.ToString()),
            };
            if (companyId != null)
            {
                claims.Add(new Claim("CompanyId", companyId.ToString()));
            }

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
    }
}