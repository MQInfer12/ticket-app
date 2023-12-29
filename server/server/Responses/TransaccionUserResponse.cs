namespace server.Responses
{
  public class TransaccionUserResponse
  {
    public Guid Id { get; set; }
    public string NombreEvento { get; set; }
    public double Total { get; set; }
    public int Cantidad { get; set; }
    public string FechaEvento { get; set; }
    public string FechaCompra { get; set; }
  }
}