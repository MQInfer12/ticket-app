using System;
using System.Collections.Generic;

namespace server.Model;

public partial class TipoEntradum
{
    public Guid Id { get; set; }

    public Guid Idtipoevento { get; set; }

    public string Nombre { get; set; } = null!;

    public double Costo { get; set; }

    public int Cantidadinicial { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Descuento> Descuentos { get; set; } = new List<Descuento>();

    public virtual TipoEvento IdtipoeventoNavigation { get; set; } = null!;
}
public class TipoEntradaDTO
{
    public string NombreEvent { get; set; }

    public double CostoEvent { get; set; }

    public int CantidadinicialEvent { get; set; }

    public int StockEvent { get; set; }
}