using System.Text.Json;
using immfApi.Endpoints;
using immfApi.Endpoints.Hangouts;

namespace immfApi.Handlers
{
    public static class HangoutHandler
    {
        public static async Task<IResult> CreateHangoutAsync(HttpContext context, IHangoutService hangoutService)
        {
            CreateHangoutRequest? request;
            try
            {
                request = await context.Request.ReadFromJsonAsync<CreateHangoutRequest>();
            }
            catch (JsonException ex)
            {
                return Results.BadRequest(new { error = "Invalid JSON", details = ex.Message });
            }

            if (request is null || request.Date > DateTime.Now)
                return Results.BadRequest("Date cannot be in the future");

            var hangout = await hangoutService.CreateHangoutAsync(request);

            return hangout is null
                    ? Results.BadRequest($"LovedOne ID is not found")
                    : Results.Created($"/hangout/{hangout.Id}", hangout);
        }

        public static async Task<IResult> GetHangoutByIdAsync(IHangoutService hangoutService, string id)
        {
            if (!int.TryParse(id, out int hangoutId)) return Results.BadRequest(new { error = "Invalid id, must be a whole number." });

            var hangout = await hangoutService.GetByIdAsync(hangoutId);

            return hangout is null
                    ? Results.NotFound($"Hangout with id {hangoutId} was not found")
                    : Results.Ok(hangout);
        }

        public static async Task<IResult> GetAllHangoutsAsync(IHangoutService hangoutService)
        {
            return Results.Ok(await hangoutService.GetAllHangoutsAsync());
        }

        public static async Task<IResult> GetAllHangoutsByLovedOneIdAsync(IHangoutService hangoutService, string id)
        {
            if (!int.TryParse(id, out int lovedOneId))
                return Results.BadRequest(new { error = "Invalid id, must be a whole number" });

            return Results.Ok(await hangoutService.GetAllHangoutsByLovedOneIdAsync(lovedOneId));
        }

        public static async Task<IResult> DeleteHangoutAsync(IHangoutService hangoutService, string id)
        {
            if (!int.TryParse(id, out int hangoutId))
                return Results.BadRequest(new { error = "Invalid id, must be a whole number" });

            var result = await hangoutService.DeleteHangoutAsync(hangoutId);

            if (result == OperationResult.NotFound)
                return Results.NotFound($"Hangout with id {hangoutId} was not found");

            return Results.Ok();
        }
    }
}