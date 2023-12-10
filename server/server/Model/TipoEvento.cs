using System;
using System.Collections.Generic;

namespace server.Model;

public partial class TipoEvento
{
    public Guid Id { get; set; }

    public Guid Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Fecha { get; set; } = null!;

    public int Cantidad { get; set; }

    public virtual Empresa IdempresaNavigation { get; set; } = null!;

    public virtual ICollection<TipoEntradum> TipoEntrada { get; set; } = new List<TipoEntradum>();
}
