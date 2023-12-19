namespace server.Responses
{
    public class UsuarioCajaResponse
    {

        public Guid Id { get; set; }
        public Guid CajaId { get; set; }
        public Guid UserId { get; set; }
        public string CajaName { get; set; }
        public string UserName { get; set; }
        public string Ci { get; set; }
        public string PersonName { get; set; }
        public string ?PersonAppaterno { get; set; }
        public string ?PersonApmaterno { get; set; }
        public Guid? CompanyId { get; set; } = null;

        public string? CompanyName { get; set; } = null;
    }
}