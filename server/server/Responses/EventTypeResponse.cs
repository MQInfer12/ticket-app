namespace server.Responses
{
    public class EventTypeResponse
    {
        public Guid Id { get; set; }
        public string TypeEventName { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}

