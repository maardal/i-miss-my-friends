using System.Text.Json;
using immfApi.Endpoints;
using immfApi.Endpoints.Hangouts;
using immfApi.Models;

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

            var validation = ValidateHangoutRequest(request);

            if (!validation.IsSuccess)
                return Results.BadRequest(new { error = validation.Error });

            var hangout = await hangoutService.CreateHangoutAsync(request);

            return hangout is null
                    ? Results.BadRequest($"LovedOne ID is not found")
                    : Results.Created($"/hangout/{hangout.Id}", hangout);
        }

        public static async Task<IResult> GetHangoutByIdAsync(IHangoutService hangoutService, string id)
        {
            var validation = ValidateId(id);
            if (!validation.IsSuccess) return Results.BadRequest(new { error = validation.Error });

            var hangout = await hangoutService.GetByIdAsync(validation.Value);

            return hangout is null
                    ? Results.NotFound($"Hangout with id {id} was not found")
                    : Results.Ok(hangout);
        }

        public static async Task<IResult> GetAllHangoutsAsync(IHangoutService hangoutService)
        {
            return Results.Ok(await hangoutService.GetAllHangoutsAsync());
        }

        public static async Task<IResult> GetAllHangoutsByLovedOneIdAsync(IHangoutService hangoutService, string id)
        {
            var validation = ValidateId(id);
            
            return !validation.IsSuccess
                    ? Results.BadRequest(new { error = validation.Error })
                    : Results.Ok(await hangoutService.GetAllHangoutsByLovedOneIdAsync(validation.Value));
        }

        public static async Task<IResult> DeleteHangoutAsync(IHangoutService hangoutService, string id)
        {
            var validation = ValidateId(id);
            if (!validation.IsSuccess) return Results.BadRequest(new { error = validation.Error });

            var result = await hangoutService.DeleteHangoutAsync(validation.Value);

            if (result == OperationResult.NotFound)
                return Results.NotFound($"Hangout with id {id} was not found");

            return Results.Ok();
        }

        private static ValidationResult<bool> ValidateHangoutRequest(CreateHangoutRequest request)
        {
            if (request is null || request.Date > DateTime.Now)
                return ValidationResult<bool>.Fail("Invalid date. Date cannot be in the future");

            return ValidationResult<bool>.Success(true);
        }

        private static ValidationResult<int> ValidateId(string id)
        {
            return !int.TryParse(id, out int lovedOneId)
                    ? ValidationResult<int>.Fail("Invalid id, must be a whole number")
                    : ValidationResult<int>.Success(lovedOneId);
        }
    }
}