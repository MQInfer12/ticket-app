namespace server.Responses
{
  public class ItemResponse
  {
    public Guid Id { get; set; }
    public Guid IdCategoria {get;set;}
    public string NombreEmpresa { get; set; }
    public string NombreCategoria { get; set; }
    public string Detalle { get; set; }
    public string FechaRegistro { get; set; }
    public int CantidadInicial { get; set; }
    public int Stock { get; set; }
    public double Costo { get; set; }
  }
}