using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Caja
{
    public Guid Id { get; set; }

    public Guid Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Empresa IdempresaNavigation { get; set; } = null!;

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();

    public virtual ICollection<UsuarioCaja> UsuarioCajas { get; set; } = new List<UsuarioCaja>();
}
