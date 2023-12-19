using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioCajaController : Controller
    {
        private DBContext _db;

        public UsuarioCajaController(DBContext db)
        {
            _db = db;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var registerUser = _db.UsuarioCajas
                .Select(x => new UsuarioCajaResponse
                {
                    Id = x.Id,
                    CajaId = x.Idcaja,
                    UserId = x.Idusuario,
                    CajaName = x.IdcajaNavigation.Nombre,
                    UserName = x.IdusuarioNavigation.NombreUsuario,
                    Ci = x.IdusuarioNavigation.IdpersonaNavigation.Ci,
                    PersonApmaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                    PersonAppaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                    PersonName = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                    CompanyId = x.IdcajaNavigation.Idempresa,
                    CompanyName = x.IdcajaNavigation.IdempresaNavigation.Nombre
                });
            return Ok(new BaseResponse<IQueryable<UsuarioCajaResponse>>
            { Message = "Lista de contactos obtenida correctamente", Data = registerUser, Status = 200 });
        }

        [HttpGet("{GetByCompanyId}"), Authorize]
        public IActionResult GetByCompany(Guid GetByCompanyId)
        {
            var registers = _db.UsuarioCajas
                .Where(v => v.IdcajaNavigation.Idempresa == GetByCompanyId)
                .Select(x => new UsuarioCajaResponse
                {
                    Id = x.Id,
                    CajaId = x.Idcaja,
                    UserId = x.Idusuario,
                    CajaName = x.IdcajaNavigation.Nombre,
                    UserName = x.IdusuarioNavigation.NombreUsuario,
                    Ci = x.IdusuarioNavigation.IdpersonaNavigation.Ci,
                    PersonApmaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                    PersonAppaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                    PersonName = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                });
            return Ok(new BaseResponse<IQueryable<UsuarioCajaResponse>>
            { Message = "Lista de contactos obtenida correctamente", Data = registers, Status = 200 });
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] UsuarioCajaDTO req)
        {
            var findService = _db.Cajas.Find(req.CajaId);

            if (findService == null)
            {
                return NotFound(new BaseResponse<string>
                { Message = "No se encontro esa caja", Data = " ", Status = 404 });
            }

            var findUser = _db.Usuarios.Find(req.UserId);

            if (findUser == null)
            {
                return NotFound(new BaseResponse<string>
                { Message = "No se encontro el usuario", Data = " ", Status = 404 });
            }

            var UserRegister = new UsuarioCaja
            {
                Idcaja = req.CajaId,
                Idusuario = req.UserId
            };

            _db.UsuarioCajas.Add(UserRegister);
            _db.SaveChanges();


            var userRegisterReturn = _db.UsuarioCajas
                .Where(v => v.Id == UserRegister.Id)
                .Select(x => new UsuarioCajaResponse
                {
                    Id = x.Id,
                    CajaId = x.Idcaja,
                    UserId = x.Idusuario,
                    CajaName = x.IdcajaNavigation.Nombre,
                    UserName = x.IdusuarioNavigation.NombreUsuario,
                    Ci = x.IdusuarioNavigation.IdpersonaNavigation.Ci,
                    PersonApmaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                    PersonAppaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                    PersonName = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                    CompanyId = x.IdcajaNavigation.IdempresaNavigation.Id,
                    CompanyName = x.IdcajaNavigation.IdempresaNavigation.Nombre
                }).First();

            return Ok(new BaseResponse<UsuarioCajaResponse>
            { Message = "Se realizo la operacion con exito", Data = userRegisterReturn, Status = 201 });
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, [FromBody] UsuarioCajaDTO req)
        {
            var existingUserService = _db.UsuarioCajas.Find(id);

            if (existingUserService == null)
            {
                return NotFound(new { Message = "No se encontraron los datos", Data = ' ', Status = 404 });
            }

            existingUserService.Idusuario = req.UserId;
            existingUserService.Idcaja = req.CajaId;

            _db.UsuarioCajas.Update(existingUserService);
            _db.SaveChanges();


            var userRegisterReturn = _db.UsuarioCajas
                .Where(v => v.Id == existingUserService.Id)
                .Select(x => new UsuarioCajaResponse
                {
                    Id = x.Id,
                    CajaId = x.Idcaja,
                    UserId = x.Idusuario,
                    CajaName = x.IdcajaNavigation.Nombre,
                    UserName = x.IdusuarioNavigation.NombreUsuario,
                    Ci = x.IdusuarioNavigation.IdpersonaNavigation.Ci,
                    PersonApmaterno = x.IdusuarioNavigation.IdpersonaNavigation.Apmaterno,
                    PersonAppaterno = x.IdusuarioNavigation.IdpersonaNavigation.Appaterno,
                    PersonName = x.IdusuarioNavigation.IdpersonaNavigation.Nombres,
                    CompanyId = x.IdcajaNavigation.IdempresaNavigation.Id,
                    CompanyName = x.IdcajaNavigation.IdempresaNavigation.Nombre
                }).First();

            return Ok(new BaseResponse<UsuarioCajaResponse>
            { Message = "Se realizo la operacion con exito", Data = userRegisterReturn, Status = 200 });
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {

            var registerUser = _db.UsuarioCajas.Find(id);
            if (registerUser == null)
            {
                return NotFound(new { Message = "No se encontro ningun dato", Data = ' ', Status = 404 });
            }

            _db.UsuarioCajas.Remove(registerUser);
            _db.SaveChanges();
            return Ok(new { Message = "Se elimino la informacion", Data = ' ', Status = 200 });

        }
    }
}