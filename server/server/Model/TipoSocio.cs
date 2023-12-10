using System;
using System.Collections.Generic;

namespace server.Model;

public partial class TipoSocio
{
    public Guid Id { get; set; }

    public Guid Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public double Costo { get; set; }

    public double? Descuento { get; set; }

    public int? Duracion { get; set; }

    public int? Partidos { get; set; }

    public bool? Estado { get; set; }

    public virtual Empresa IdempresaNavigation { get; set; } = null!;

    public virtual ICollection<Socio> Socios { get; set; } = new List<Socio>();
}
