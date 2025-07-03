using immfApi.DataAccessLayer;
using immfApi.Models;

namespace immfApi.Endpoints.LovedOnes
{
    public class LovedOneService : ILovedOneService
    {
        private readonly ILovedOneRepository _lovedOneRepository;

        public LovedOneService(ILovedOneRepository lovedOneRepository)
        {
            _lovedOneRepository = lovedOneRepository;
        }

        public async Task<CreateLovedOneResponse> CreateAsync(CreateLovedOneRequest request)
        {
            var lovedOne = new LovedOne { Name = request.Name, Relationship = EnumTools.MapStringToEnumRelationship(request.Relationship) };
            var createdLovedOne = await _lovedOneRepository.AddAsync(lovedOne);
            var lastHangout = createdLovedOne.Hangouts?.OrderByDescending(hangout => hangout.Date).Select(hangout => hangout.Date).FirstOrDefault();
            return new CreateLovedOneResponse(createdLovedOne.Id, createdLovedOne.Name, EnumTools.MapEnumToStringRelationship(createdLovedOne.Relationship), lastHangout);
        }

        public async Task<GetSingleLovedOneResponse?> GetByIdAsync(LovedOneIdRequest request)
        {
            var lovedOne = await _lovedOneRepository.GetByIdAsync(request.LovedOneId);
            if (lovedOne == null) return null;
            var lastHangout = lovedOne.Hangouts?.OrderByDescending(hangout => hangout.Date).Select(hangout => hangout.Date).FirstOrDefault();
            return new GetSingleLovedOneResponse(lovedOne.Id, lovedOne.Name, EnumTools.MapEnumToStringRelationship(lovedOne.Relationship), lastHangout);
        }

        public async Task<GetAllLovedOnesResponse> GetAllAsync()
        {
            var lovedOnes = await _lovedOneRepository.GetAllAsync();
            var lovedOnesList = lovedOnes.Select(lovedOne => new GetSingleLovedOneResponse
            (
                lovedOne.Id,
                lovedOne.Name,
                EnumTools.MapEnumToStringRelationship(lovedOne.Relationship),
                lovedOne.Hangouts?.OrderByDescending(hangout => hangout.Date).Select(hangout => hangout.Date).FirstOrDefault()
            )).ToList();
            return new GetAllLovedOnesResponse(lovedOnesList);
        }

        public async Task<UpdateLovedOneResponse?> UpdateAsync(UpdateLovedOneRequest request)
        {
            var lovedOne = await _lovedOneRepository.UpdateAsync(request.Id, request.Name, EnumTools.MapStringToEnumRelationship(request.Relationship));
            if (lovedOne == null) return null;
            return new UpdateLovedOneResponse($"Successfully updated lovedone with ID {lovedOne.Id}, with data {lovedOne.Name} & {lovedOne.Relationship}");
        }

        public async Task<OperationResult> DeleteAsync(LovedOneIdRequest request)
        {
            var lovedOne = await _lovedOneRepository.DeleteAsync(request.LovedOneId);
            if (lovedOne == OperationResult.NotFound) return OperationResult.NotFound;
            return OperationResult.Deleted;
        }
    }

    public interface ILovedOneService
    {
        Task<CreateLovedOneResponse> CreateAsync(CreateLovedOneRequest request);
        Task<GetSingleLovedOneResponse?> GetByIdAsync(LovedOneIdRequest request);
        Task<GetAllLovedOnesResponse> GetAllAsync();
        Task<UpdateLovedOneResponse?> UpdateAsync(UpdateLovedOneRequest request);
        Task<OperationResult> DeleteAsync(LovedOneIdRequest request);
    }
}