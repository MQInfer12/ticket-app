using System;
using System.Collections.Generic;

namespace server.Model;

public partial class DetalleTransaccione
{
    public Guid Id { get; set; }

    public Guid Idtransaccion { get; set; }

    public string Detalle { get; set; } = null!;

    public int Cantidad { get; set; }

    public double Preciounitario { get; set; }

    public string Ci { get; set; } = null!;

    public bool? Verificado { get; set; }

    public virtual Transaccion IdtransaccionNavigation { get; set; } = null!;
}
