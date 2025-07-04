using immfApi.Endpoints;
using immfApi.Endpoints.LovedOnes;
using System.Text.Json;

namespace immfApi.Handlers
{
    public static class LovedOneHandlers
    {
        public static async Task<IResult> CreateLovedOneRequestAsync(HttpContext context, ILovedOneService lovedOneService)
        {
            CreateLovedOneRequest? request;
            try
            {
                request = await context.Request.ReadFromJsonAsync<CreateLovedOneRequest>();
            }
            catch (JsonException ex)
            {
                return Results.BadRequest(new { error = "Invalid JSON", details = ex.Message });
            }

            if (request is null || string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 99)
                return Results.BadRequest("Name must be between 1 and 99 (inclusive) characters long");

            if (string.IsNullOrWhiteSpace(request.Relationship) || !EnumTools.IsValidRelationship(request.Relationship))
                return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");


            var lovedOne = await lovedOneService.CreateAsync(request);

            return Results.Created($"/lovedone/{lovedOne.Id}", lovedOne);
        }

        public static async Task<IResult> GetByIdAsync(ILovedOneService lovedOneService, string id)
        {
            if (!int.TryParse(id, out var lovedOneId)) return Results.BadRequest(new { error = "Invalid id, must be a whole number." });
            var lovedOne = await lovedOneService.GetByIdAsync(lovedOneId);

            return lovedOne is null
                ? Results.NotFound($"Lovedone with id {lovedOneId} was not Found")
                : Results.Ok(lovedOne);
        }

        public static async Task<IResult> GetAllLovedOnesAsync(ILovedOneService lovedOneService)
        {
            return Results.Ok(await lovedOneService.GetAllAsync());
        }

        public static async Task<IResult> UpdateLovedOneAsync(HttpContext context, ILovedOneService lovedOneService)
        {
            UpdateLovedOneRequest? request;

            try
            {
                request = await context.Request.ReadFromJsonAsync<UpdateLovedOneRequest>();
            }
            catch (JsonException ex)
            {
                return Results.BadRequest(new { error = "Invalid JSON", details = ex.Message });
            }

            if (request is null || string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 99)
                return Results.BadRequest("Name must be between 1 and 99 (inclusive) characters long");

            if (string.IsNullOrWhiteSpace(request.Relationship) || !EnumTools.IsValidRelationship(request.Relationship))
                return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");

            var lovedOne = await lovedOneService.UpdateAsync(request);

            return lovedOne is null
                ? Results.NotFound()
                : Results.NoContent();
        }

        public static async Task<IResult> DeleteLovedOneAsync(ILovedOneService lovedOneService, string id)
        {
            if (!int.TryParse(id, out var lovedOneId)) return Results.BadRequest(new { error = "Invalid id, must be a whole number." });
            var result = await lovedOneService.DeleteAsync(lovedOneId);
            if (result == OperationResult.NotFound) return Results.NotFound();
            return Results.Ok();
        }
    }
}