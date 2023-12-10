using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Socio
{
    public Guid Id { get; set; }

    public Guid Idtiposocio { get; set; }

    public Guid Idpersona { get; set; }

    public string Fechainicio { get; set; } = null!;

    public string? Fechafinal { get; set; }

    public int? Partidos { get; set; }

    public bool? Estado { get; set; }

    public virtual Persona IdpersonaNavigation { get; set; } = null!;

    public virtual TipoSocio IdtiposocioNavigation { get; set; } = null!;
}
