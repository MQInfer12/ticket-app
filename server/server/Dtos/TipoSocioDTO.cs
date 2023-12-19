namespace server.Dtos{
    public class TipoSocioDTO{
        public Guid CompanyId { get; set; }

        public string Name { get; set; } = null!;

        public double Cost { get; set; }

        public double? Discount { get; set; }

        public int? Duration { get; set; }

        public int? Games { get; set; }

        public bool? State { get; set; } = true;

    }
}