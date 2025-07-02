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

                if (!Enum.IsDefined(typeof(Relationship), request.Relationship.ToLower()))
                {
                    return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");
                }

                var lovedOne = await lovedOneService.CreateAsync(request);

                return Results.Created($"/lovedone/{lovedOne.Id}", lovedOne);
            });

            app.MapGet("/lovedone/{id}", async ([FromServices] ILovedOneService lovedOneService, [AsParameters] GetByIdRequest request) =>
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

            // app.MapPut("/lovedone/{id}", async (IMissMyFriendsDbContext db, int lovedOneId, string name, string relationship) =>
            // {
            //     if (!Enum.IsDefined(typeof(Relationship), relationship))
            //     {
            //         return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");
            //     }

            //     var oldLoved = await db.LovedOnes.FindAsync(lovedOneId);
            //     if (oldLoved is null) return Results.NotFound();
            //     oldLoved.Name = name;
            //     oldLoved.Relationship = EnumTools.MapStringToEnumRelationship(relationship);
            //     await db.SaveChangesAsync();
            //     return Results.NoContent();

            // });

            // app.MapDelete("/lovedone/{id}", async (IMissMyFriendsDbContext db, int id) =>
            // {
            //     var loved = await db.LovedOnes.FindAsync(id);
            //     if (loved is null) return Results.NotFound();
            //     db.LovedOnes.Remove(loved);
            //     await db.SaveChangesAsync();
            //     return Results.Ok();
            // });
        }
    }
}