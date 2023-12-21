namespace server.Responses
{
  public class TransaccionResponse
  {
    public string NombreEmpresa { get; set; }
    public string NombreUsuario { get; set; }
    public double Total { get; set; }
    public int Cantidad { get; set; }
    public double Extra { get; set; }
    public string TipoEntrega { get; set; }
    public string Fecha { get; set; }
  }
}