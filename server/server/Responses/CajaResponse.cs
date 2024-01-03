namespace server.Responses
{
    public record CajaResponse(
        Guid Id,
        Guid CompanyId,
        string CajaName,
        string CompanyName);
}