namespace server.Responses
{
    public class EntradaResponse
    {
        public Guid Id { get; set; }
        public Guid IdTipoEvento { get; set; }
        public string NombreEvento { get; set; }
        public string NombreEmpresa { get; set; }
        public string Nombre { get; set; }
        public double Costo { get; set; }
        public int CantidadInicial { get; set; }
        public int Stock { get; set; }
    }
}