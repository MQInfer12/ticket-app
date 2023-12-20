using System;
using System.Collections.Generic;

namespace server.Model;

public partial class RolUsuario
{
    public Guid Id { get; set; }

    public Guid Idusuario { get; set; }

    public Guid Idtiporol { get; set; }

    public Guid? Idempresa { get; set; }

    public bool? Estado { get; set; }

    public virtual Empresa IdempresaNavigation { get; set; } = null!;

    public virtual TipoRol IdtiporolNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}

public class RolUsuarioDTO
{
    public Guid Idusuario { get; set; }
    public Guid Rol { get; set; }
    public Guid Empresa { get; set; }
    public Boolean Estado { get; set; } = true;
}