namespace server.Responses
{
    public class TipoSocioResponse
    {
        public Guid Id { get; set; }

        public string TypePartnerName { get; set; } = null!;

        public double TypePartnerCost { get; set; }

        public double? TypePartnerDiscount { get; set; }

        public int? TypePartnerDuration { get; set; }

        public int? TypePartnerGames { get; set; }

        public bool? TypePartnerState { get; set; }

        public Guid ComapanyId { get; set; }

        public string CompanyName { get; set; }
    }
}