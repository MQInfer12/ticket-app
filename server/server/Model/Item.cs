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
public class ItemDTO
{
    public Guid IdCategoria { get; set; }

    public string DetalleItem { get; set; } = null!;

    public string FechaRegistroItem { get; set;  } = null!;

    public int CantidadinicialItem { get; set; } 

    public int StockItem { get; set;} 

    public double CostoItem { get; set; }
}
