using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Cuentum
{
    public Guid Id { get; set; }

    public Guid? Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual Empresa? IdempresaNavigation { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
