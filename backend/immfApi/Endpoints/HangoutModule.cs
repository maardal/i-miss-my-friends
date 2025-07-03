using immfApi.Endpoints.Hangouts;
using Microsoft.AspNetCore.Mvc;

namespace immfApi.Endpoints
{
    public static class HangoutModule
    {
        public static void AddHangoutEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/hangout", async ([FromServices] IHangoutService hangoutService, [FromBody] CreateHangoutRequest request) =>
            {
                var hangout = await hangoutService.CreateHangoutAsync(request);
                if (hangout == null) return Results.BadRequest($"LovedOne ID is not found");
                return Results.Created($"/hangout/{hangout.Id}", hangout);
            });

            app.MapGet("hangout/{id}", async ([FromServices] IHangoutService hangoutService, [AsParameters] HangoutIdRequest request) =>
            {
                var hangout = await hangoutService.GetByIdAsync(request);
                if (hangout == null) return Results.NotFound($"Hangout with id {request.HangoutId} was not found");
                return Results.Ok(hangout);
            });

            app.MapGet("/hangouts/", async ([FromServices] IHangoutService hangoutService) =>
            {
                var hangouts = await hangoutService.GetAllHangoutsAsync();
                return Results.Ok(hangouts);
            });

            app.MapGet("/hangouts/{id}", async ([FromServices] IHangoutService hangoutService, [AsParameters] LovedOneIdRequest request) =>
            {
                var hangouts = await hangoutService.GetAllHangoutsByLovedOneIdAsync(request);
                return Results.Ok(hangouts);
            });

            app.MapDelete("/hangout/{id}", async ([FromServices] IHangoutService hangoutService, [AsParameters] HangoutIdRequest request) =>
            {
                var result = await hangoutService.DeleteHangoutAsync(request);
                if (result == OperationResult.NotFound) return Results.NotFound($"Hangout with id {request.HangoutId} was not found");
                return Results.Ok();

            });
        }
    }
}