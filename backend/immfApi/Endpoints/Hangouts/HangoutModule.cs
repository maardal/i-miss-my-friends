using immfApi.Endpoints.Hangouts;
using immfApi.Handlers;

namespace immfApi.Endpoints
{
    public static class HangoutModule
    {
        public static void AddHangoutEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/hangout", HangoutHandler.CreateHangoutAsync)
            .Accepts<CreateHangoutRequest>("application/json")
            .Produces<HangoutResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            app.MapGet("hangout/{id}", HangoutHandler.GetHangoutByIdAsync)
            .Produces<HangoutResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            app.MapGet("/hangouts/", HangoutHandler.GetAllHangoutsAsync)
            .Produces<HangoutsResponse>(StatusCodes.Status200OK);

            app.MapGet("/hangouts/{id}", HangoutHandler.GetAllHangoutsByLovedOneIdAsync)
            .Produces<HangoutsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            app.MapDelete("/hangout/{id}", HangoutHandler.DeleteHangoutAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}