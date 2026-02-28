using immfApi.Endpoints.LovedOnes;
using immfApi.Handlers;

namespace immfApi.Endpoints
{
    public static class LovedOneModule
    {
        public static void AddLovedOneEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/lovedone", LovedOneHandlers.CreateLovedOneRequestAsync)
            .Accepts<CreateLovedOneRequest>("application/json")
            .Produces<CreateLovedOneResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            app.MapGet("/lovedone/{id}", LovedOneHandlers.GetByIdAsync)
            .Produces<GetSingleLovedOneResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            app.MapGet("/lovedones", LovedOneHandlers.GetAllLovedOnesAsync)
            .Produces<GetAllLovedOnesResponse>(StatusCodes.Status200OK);

            app.MapPut("/lovedone", LovedOneHandlers.UpdateLovedOneAsync)
            .Accepts<UpdateLovedOneRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            app.MapDelete("/lovedone/{id}", LovedOneHandlers.DeleteLovedOneAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
            
        }
    }
}
