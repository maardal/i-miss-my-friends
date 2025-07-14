using immfApi.Models;
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

            var validation = ValidateCreateLovedOneRequest(request);
            if (!validation.IsSuccess) return Results.BadRequest(new { error = validation.Error });

            var lovedOne = await lovedOneService.CreateAsync(request);

            return Results.Created($"/lovedone/{lovedOne.Id}", lovedOne);
        }

        public static async Task<IResult> GetByIdAsync(ILovedOneService lovedOneService, string id)
        {
            var validation = ValidateId(id);
            if (!validation.IsSuccess) return Results.BadRequest(new { error = validation.Error });

            var lovedOne = await lovedOneService.GetByIdAsync(validation.Value);

            return lovedOne is null
                ? Results.NotFound($"Lovedone with id {id} was not Found")
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

            var validation = ValidateUpdateLovedOneRequest(request);
            if (!validation.IsSuccess)
                return Results.BadRequest(new { error = validation.Error });

            var lovedOne = await lovedOneService.UpdateAsync(request);

            return lovedOne is null
                ? Results.NotFound()
                : Results.NoContent();
        }

        public static async Task<IResult> DeleteLovedOneAsync(ILovedOneService lovedOneService, string id)
        {
            var validation = ValidateId(id);
            if (!validation.IsSuccess) return Results.BadRequest(new { error = validation.Error });

            var result = await lovedOneService.DeleteAsync(validation.Value);
            if (result == OperationResult.NotFound) return Results.NotFound();

            return Results.Ok();
        }

        private static ValidationResult<CreateLovedOneRequest> ValidateCreateLovedOneRequest(CreateLovedOneRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 99)
                return ValidationResult<CreateLovedOneRequest>.Fail("Name must be between 1 and 99 (inclusive) characters long");

            if (string.IsNullOrWhiteSpace(request.Relationship) || !EnumTools.IsValidRelationship(request.Relationship))
                return ValidationResult<CreateLovedOneRequest>.Fail($"Relationship must be one of: {EnumTools.EnumListPrettified()}");

            return ValidationResult<CreateLovedOneRequest>.Success(request);
        }

        private static ValidationResult<UpdateLovedOneRequest> ValidateUpdateLovedOneRequest(UpdateLovedOneRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 99)
                return ValidationResult<UpdateLovedOneRequest>.Fail("Name must be between 1 and 99 (inclusive) characters long");

            if (string.IsNullOrWhiteSpace(request.Relationship) || !EnumTools.IsValidRelationship(request.Relationship))
                return ValidationResult<UpdateLovedOneRequest>.Fail($"Relationship must be one of: {EnumTools.EnumListPrettified()}");

            return ValidationResult<UpdateLovedOneRequest>.Success(request);
        }

        private static ValidationResult<int> ValidateId(string id)
        {
            return !int.TryParse(id, out var lovedOneId)
                    ? ValidationResult<int>.Fail("Invalid id, must be whole number")
                    : ValidationResult<int>.Success(lovedOneId);
        }
    }
}