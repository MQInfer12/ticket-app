using System;
using System.Collections.Generic;

namespace server.Model;

public partial class UsuarioCaja
{
    public Guid Id { get; set; }

    public Guid Idcaja { get; set; }

    public Guid Idusuario { get; set; }

    public virtual Caja IdcajaNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
