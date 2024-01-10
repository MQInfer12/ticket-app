using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Constants;
using server.Dtos;
using server.Helpers;
using server.Helps;
using server.Model;
using System.Data;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DBContext _db;
        private readonly TokenHelper _tokenHelper;
        HttpClass<Usuario> httpConst = new HttpClass<Usuario>();

        public UserController(DBContext db, TokenHelper tokenHelper)
        {
            _db = db;
            _tokenHelper = tokenHelper;
        }

        [HttpGet, Authorize]
        [Route("GetUserByToken")]
        public IActionResult GetUserToken()
        {
            string userRolId = User.FindFirst("UserRolId").Value; //get id
            string companyId = User.FindFirst("CompanyId")?.Value; //get id
            if (companyId != null)
            {
                var userRes = _db.RolUsuarios
                    .Where(u => u.Id == Guid.Parse(userRolId))
                    .Where(u => u.Idempresa == Guid.Parse(companyId))
                    .Select(x => new
                    {
                        UserId = x.Idusuario,
                        RoleTypeId = x.Idtiporol,
                        CompanyId = x.Idempresa,
                        RoleName = x.IdtiporolNavigation.Nombre,
                        UserName = x.IdusuarioNavigation.NombreUsuario,
                        Password = x.IdusuarioNavigation.Contrasenia,
                        CompanyName = x.IdempresaNavigation.Nombre,
                        CompanyAddress = x.IdempresaNavigation.Direccion,
                        CompanyState = x.IdempresaNavigation.Estado,
                        PersonName = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                        PersonLastName = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                        PersonLast = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                        IdRolUser = x.Id,
                    }).First();
                return Ok(new { Message = "Token obtenido", Data = userRes, Status = 200 });
            }
            else
            {
                var userRes = _db.RolUsuarios
                    .Where(u => u.Id == Guid.Parse(userRolId))
                    .Select(x => new
                    {
                        UserId = x.Idusuario,
                        RoleTypeId = x.Idtiporol,
                        CompanyId = x.Idempresa,
                        RoleName = x.IdtiporolNavigation.Nombre,
                        UserName = x.IdusuarioNavigation.NombreUsuario,
                        Password = x.IdusuarioNavigation.Contrasenia,
                        CompanyName = x.IdempresaNavigation.Nombre,
                        CompanyAddress = x.IdempresaNavigation.Direccion,
                        CompanyState = x.IdempresaNavigation.Estado,
                        PersonName = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                        PersonLastName = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                        PersonLast = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                        IdRolUser = x.Id,
                    }).First();
                return Ok(new { Message = "Token obtenido", Data = userRes, Status = 200 });
            }
        }


        [HttpGet]
        [Route("GetimgPerson/{userId}")]
        public IActionResult GetImgPerson(Guid userId)
        {
            var userRes = _db.Usuarios
                .Where(v => v.Id == userId)
                .Join(_db.Personas,
                user => user.Idpersona,
                person => person.Id,
                (user, person) => new
                {
                    Foto = person.Foto,
                }
                ).FirstOrDefault();

            if (userRes == null)
            {
                return NotFound(httpConst.NotfoundFunc("No se encontro el id de ese usuario"));
            }

            if (userRes.Foto == null)
            {
                return NotFound(httpConst.NotfoundFunc("No se encontro la foto de este usuario"));

            }
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "UserImage", userRes.Foto
                );

            return PhysicalFile(path, "image/png");
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

                var passwordDecrypt = HashHelper.Decrypt(user.Contrasenia);

                if (passwordDecrypt == req.Contrasenia)
                {
                    var userRols = _db.RolUsuarios.Where(ru => ru.IdusuarioNavigation.Idpersona == user.Idpersona).Select(
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
                        return NotFound(httpConst.NotfoundFunc("Este usuario no tiene ningun rol"));
                    }
                    return Ok(new { Message = "Bienvenido", Data = userRols, Status = 200 });
                }
                return BadRequest(new { Message = "Contraseña incorrecta", Data = ' ', Status = 409 });

            }
            return NotFound(httpConst.NotfoundFunc("No se encontro el usuario"));
        }

        [HttpGet("LoginByRole/{rolUserId}")]
        public IActionResult LoginByRole(Guid rolUserId)
        {
            //get the id RoleType from the table UserRol
            var userRol = _db.RolUsuarios.FirstOrDefault(v => v.Id == rolUserId);

            if (userRol == null)
                return NotFound(httpConst.NotfoundFunc("No se encontro el rol de ese usuario"));

            //get the name of the rol iof the table roleType
            var roletype = _db.TipoRols.FirstOrDefault(v => v.Id == userRol.Idtiporol);

            if (roletype == null)
                return NotFound(httpConst.NotfoundFunc("No se encontro el nombre de ese rol"));

            var user = _db.Usuarios.Find(userRol.Idusuario);


            var token = _tokenHelper.GenerateToken(userRol, roletype.Nombre, roletype.Id, userRol.Idempresa);
            return Ok(new { Message = "Bienvenido", Data = token, Status = 200 });
        }

        private string saveImg(string fileName, RegisterDTO model)
        {
            var uniqueFileName = FileHelper.GetUniqueFileName(fileName);

            var filePath = Path.Combine("wwwroot", "UserImage", uniqueFileName);

            //var filePath = Path.Combine(uploads, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.Image.CopyTo(stream);

            }

            //Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            return uniqueFileName;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] RegisterDTO req)
        {
            // ====================== SAVE IN PEOPLE TABLE ======================
            var existPeople = _db.Personas.Any(e => e.Ci == req.Ci);
            if (existPeople)
                return BadRequest(httpConst.BadRequest("Ya existe este CI"));

            var existUser = _db.Usuarios.Any(e => e.NombreUsuario == req.Usuario);
            if (existUser)
                return BadRequest(httpConst.BadRequest("Ya existe este nombre de usuario"));

            if (req.Contrasenia != req.ContraseniaRepit)
                return BadRequest(httpConst.BadRequest("Las contraseñas no coindicen"));


            //get role type
            var roleType = _db.TipoRols.FirstOrDefault((v) => v.Nombre == "Cliente");
            if (roleType == null)
                return NotFound(httpConst.NotfoundFunc("No se encontro el rol Cliente"));


            //save img
            var filePathImg = saveImg(req.Image.FileName, req);

            var people = new Persona
            {
                Ci = req.Ci,
                Apmaterno = req.Apmaterno,
                Appaterno = req.Appaterno,
                Nombres = req.Nombres,
                Foto = filePathImg
            };

            _db.Personas.Add(people);
            _db.SaveChanges();

            // ====================== SAVE IN USER TABLE ======================
            //Generate password

            var passwordHash = HashHelper.Encrypt(req.Contrasenia);

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
                Estado = true
            };
            _db.RolUsuarios.Add(userRole);
            _db.SaveChanges();

            var token = _tokenHelper.GenerateToken(userRole, roleType.Nombre, roleType.Id, userRole.Idempresa);

            return Ok(new { Message = "Se registro su cuenta con exito", Data = token, Status = 200 });
        }
    }
}
