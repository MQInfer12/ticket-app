using server.Model;

namespace server.Dtos
{
    public class ItemDTO
    {

        public Guid Idcategoria { get; set; }

        public string Detalle { get; set; }
        public string Fecharegistro { get; set; }
        public int Cantidadinicial { get; set; }
        public int Stock { get; set; }
        public double Costo { get; set; }


    }
}