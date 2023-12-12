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
public class PersonaDTO // para decir al front que me tiene que enviar
{
    public string Ci { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? Appaterno { get; set; }

    public string? Apmaterno { get; set; }


    public string? NombreUsurio { get; set; }

    public string? Password { get; set; }
}
