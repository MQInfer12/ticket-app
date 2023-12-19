using server.Model;

namespace server.Dtos
{
    public class EventTypeDTO
    {

            public Guid Idempresa { get; set; }

            public string Nombre { get; set; } = null!;

            public string Fecha { get; set; } = null!;

            public int Cantidad { get; set; }
    }
}
