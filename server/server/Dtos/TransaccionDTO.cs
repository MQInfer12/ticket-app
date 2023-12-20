using server.Model;

namespace server.Dtos
{
  public class CarritoDTO
  {

    public Guid IdUsuario { get; set; }

    public List<Item> Items { get; set; }
  }
  public class Item
  {
    public Guid IdEntrada { get; set; }
    public int Cantidad { get; set; }
    public string[] Ci { get; set; }
  }
}
