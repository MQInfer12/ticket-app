namespace server.Responses
{
    public class CuentaResponse
    {
        public Guid Id { get; set; }
        public Guid? IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}