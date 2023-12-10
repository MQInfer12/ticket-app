using System;
using System.Collections.Generic;

namespace server.Model;

public partial class TipoRol
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<RolUsuario> RolUsuarios { get; set; } = new List<RolUsuario>();
}
