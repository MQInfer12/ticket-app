using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Persona
{
    public Guid Id { get; set; }

    public string Ci { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? Appaterno { get; set; }

    public string? Apmaterno { get; set; }

    public virtual ICollection<Socio> Socios { get; set; } = new List<Socio>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
