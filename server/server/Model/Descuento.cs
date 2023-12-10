using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Descuento
{
    public Guid Id { get; set; }

    public Guid Idtipoentrada { get; set; }

    public double Descuento1 { get; set; }

    public bool? Estado { get; set; }

    public virtual TipoEntradum IdtipoentradaNavigation { get; set; } = null!;
}
