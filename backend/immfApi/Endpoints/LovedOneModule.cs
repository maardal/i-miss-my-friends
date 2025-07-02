using immfApi.Models;
using immfApi.Endpoints.LovedOnes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace immfApi.Endpoints
{
    public static class LovedOneModule
    {
        public static void AddLovedOneEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/lovedone", async ([FromServices] ILovedOneService lovedOneService,[FromBody] CreateLovedOneRequest request) =>
            {

                if (!Enum.IsDefined(typeof(Relationship), EnumTools.MapStringToEnumRelationship(request.Relationship)))
                {
                    return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");
                }

                var lovedOne = await lovedOneService.CreateAsync(request);

                return Results.Created($"/lovedone/{lovedOne.Id}", lovedOne);
            });

            app.MapGet("/lovedone/{id}", async ([FromServices] ILovedOneService lovedOneService, [AsParameters] IdRequest request) =>
            {
                var lovedOne = await lovedOneService.GetByIdAsync(request);
                if (lovedOne == null) return Results.NotFound($"Lovedone with id {request.Id} was not Found");
                return Results.Ok(lovedOne);
            });

            app.MapGet("/lovedones", async ([FromServices] ILovedOneService lovedOneService) =>
            {
                var lovedOnes = await lovedOneService.GetAllAsync();
                return Results.Ok(lovedOnes);
            });

            app.MapPut("/lovedone", async ([FromServices] ILovedOneService lovedOneService,[FromBody] UpdateLovedOneRequest request) =>
            {
                if (!Enum.IsDefined(typeof(Relationship), EnumTools.MapStringToEnumRelationship(request.Relationship)))
                {
                    return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");
                }

                var lovedOne = await lovedOneService.UpdateAsync(request);
                if (lovedOne is null) return Results.NotFound();
                return Results.NoContent();

            });

            app.MapDelete("/lovedone/{id}", async ([FromServices] ILovedOneService lovedOneService, [AsParameters] IdRequest request) =>
            {
                var result = await lovedOneService.DeleteAsync(request);
                if (result == OperationResult.NotFound) return Results.NotFound();
                return Results.Ok();
            });
        }
    }
}