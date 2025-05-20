
namespace immfApi.Endpoints.LovedOnes
{
    public record GetSingleLovedOneResponse(int Id, string Name, string Relationship, DateTime? LastHangout);
}