using System;
using System.Collections.Generic;

namespace server.Model;

public partial class Historial
{
    public Guid Id { get; set; }

    public Guid Idempresa { get; set; }

    public Guid Idusuario { get; set; }

    public string Fecha { get; set; } = null!;

    public string Tabla { get; set; } = null!;

    public Guid Idfila { get; set; }

    public string Anterior { get; set; } = null!;

    public string Nuevo { get; set; } = null!;

    public virtual Empresa IdempresaNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
