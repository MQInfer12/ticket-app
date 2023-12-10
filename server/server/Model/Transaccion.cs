using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Transaccion
{
    public Guid Id { get; set; }

    public Guid Idcuenta { get; set; }

    public Guid Idusuario { get; set; }

    public Guid Idcaja { get; set; }

    public double Montototal { get; set; }

    public int Cantidad { get; set; }

    public double Extra { get; set; }

    public string Tipoentrega { get; set; } = null!;

    public string Fecha { get; set; } = null!;

    public virtual ICollection<DetalleTransaccione> DetalleTransacciones { get; set; } = new List<DetalleTransaccione>();

    public virtual Caja IdcajaNavigation { get; set; } = null!;

    public virtual Cuentum IdcuentaNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
