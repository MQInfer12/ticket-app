using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server.Model;

public partial class Usuario
{
    public Guid Id { get; set; }

    public Guid Idpersona { get; set; }

    public string NombreUsuario{ get; set; } = null!;

    [StringLength(250)]
    public string Contrasenia { get; set; } = null!;

    public virtual ICollection<Historial> Historials { get; set; } = new List<Historial>();

    public virtual Persona IdpersonaNavigation { get; set; } = null!;

    public virtual ICollection<RolUsuario> RolUsuarios { get; set; } = new List<RolUsuario>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();

    public virtual ICollection<UsuarioCaja> UsuarioCajas { get; set; } = new List<UsuarioCaja>();
}

public class UsuarioDTO
{
    public string Usuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

}
