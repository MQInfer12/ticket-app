using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Categorium
{
    public Guid Id { get; set; }

    public Guid Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Empresa IdempresaNavigation { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
