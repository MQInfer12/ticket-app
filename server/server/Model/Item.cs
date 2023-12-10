using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Item
{
    public Guid Id { get; set; }

    public Guid Idcategoria { get; set; }

    public string Detalle { get; set; } = null!;

    public string Fecharegistro { get; set; } = null!;

    public int Cantidadinicial { get; set; }

    public int Stock { get; set; }

    public double Costo { get; set; }

    public virtual Categorium IdcategoriaNavigation { get; set; } = null!;
}
