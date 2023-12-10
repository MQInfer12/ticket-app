using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Contacto
{
    public Guid Id { get; set; }

    public string Contacto1 { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public Guid Personaempresa { get; set; }
}
