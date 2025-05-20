namespace immfApi.Endpoints
{
    public record CreateLovedOneResponse(int Id, string Name, string Relationship, DateTime? LastHangout);
}