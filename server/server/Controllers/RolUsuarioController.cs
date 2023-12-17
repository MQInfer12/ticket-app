﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Model;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {
        //variable global
        private DBContext _db;
        //constructor inica lo primero del sistema
        public RolUsuarioController(DBContext db)
        {
            _db = db;
        }

        [HttpPost, Authorize] //    Tipo post //
        public IActionResult Post([FromBody] RolUsuarioDTO req)
        {
            var exists = _db.RolUsuarios.Any(e => e.Idtiporol == req.Rol && e.Idempresa == req.Empresa && e.Idusuario == req.Idusuario);
            if (exists) {
                return BadRequest(new { Message = "Ya existe este rol en esta empresa", Data = ' ', Status = 400 });
            }
            var userRole = new RolUsuario
            {
                Idusuario = req.Idusuario,
                Idtiporol = req.Rol,
                Idempresa = req.Empresa,
                Estado = req.Estado
            };
            _db.RolUsuarios.Add(userRole);
            _db.SaveChanges();

            var userRoleGet = _db.RolUsuarios.Where(x => x.Id == userRole.Id).Select(data =>new {
                id = data.Id,
                idRol = data.Idtiporol,
                rol = data.IdtiporolNavigation.Nombre,
                idEmpresa = data.Idempresa,
                empresa = data.IdempresaNavigation.Nombre,
                estado = data.Estado
            }).First();

            return Ok(new { Message = "Se agrego la asignacion", Data = userRoleGet, Status = 201 });
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(Guid id, RolUsuarioDTO req)
        {
            var existsRoleUser = _db.RolUsuarios.Find(id);
            if(existsRoleUser == null)
            {
                return NotFound(new { Message = "No se encontro asignacion", Data = ' ', Status = 404 });
            }
            var exists = _db.RolUsuarios.Any(e => e.Idempresa == req.Empresa && e.Idtiporol == req.Rol && e.Idusuario == req.Idusuario && e.Id != id);
            if(exists) {
                return BadRequest(new { Message = "Ya existe este rol en esta empresa", Data = ' ', Status = 400 });
            }
            existsRoleUser.Idempresa = req.Empresa;
            existsRoleUser.Idtiporol = req.Rol;
            existsRoleUser.Estado = req.Estado;

            _db.RolUsuarios.Update(existsRoleUser);
            _db.SaveChanges();

            var userRoleGet = _db.RolUsuarios.Where(x => x.Id == existsRoleUser.Id).Select(data => new {
                id = data.Id,
                idRol = data.Idtiporol,
                rol = data.IdtiporolNavigation.Nombre,
                idEmpresa = data.Idempresa, 
                empresa = data.IdempresaNavigation.Nombre,
                estado = data.Estado
            }).First();
            return Ok(new { Message = "Se edito la asignacion", Data = userRoleGet, Status = 200 });
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(Guid id)
        {
            var existsRoleUser = _db.RolUsuarios.Find(id);
            if (existsRoleUser == null)
            {
                return NotFound(new { Message = "No se encontro la asignacion", Data = ' ', Status = 404 });
            }

            var deleteRoleUser = _db.RolUsuarios.FirstOrDefault(v => v.Id == id);
            _db.RolUsuarios.Remove(deleteRoleUser);

            _db.SaveChanges();

            return Ok(new { Message = "Se elimino la asignacio", Data = ' ', Status = 200 });
        }
    }
}