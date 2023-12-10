using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Empresa
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public bool? Estado { get; set; }

   public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();

    public virtual ICollection<Historial> Historials { get; set; } = new List<Historial>();

    public virtual ICollection<RolUsuario> RolUsuarios { get; set; } = new List<RolUsuario>();

    public virtual ICollection<TipoEvento> TipoEventos { get; set; } = new List<TipoEvento>();

    public virtual ICollection<TipoSocio> TipoSocios { get; set; } = new List<TipoSocio>();
}


public class EmpresaDTO
{
    //    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public bool? Estado { get; set; }

}
