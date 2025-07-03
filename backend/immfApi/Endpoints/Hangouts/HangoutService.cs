using immfApi.Models;
using immfApi.DataAccessLayer;

namespace immfApi.Endpoints.Hangouts
{

    public class HangoutService(IHangoutRepository hangoutRepository) : IHangoutService
    {

        private readonly IHangoutRepository _hangoutRepository = hangoutRepository;

        public async Task<HangoutResponse?> CreateHangoutAsync(CreateHangoutRequest request)
        {

            var lovedOne = await _hangoutRepository.FindLovedOneAsync(request.LovedOneId);
            if (lovedOne == null) return null;

            var hangout = await _hangoutRepository.CreateHangoutAsync(new Hangout { Date = request.Date, LovedOne = lovedOne });
            return new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id);
        }

        public async Task<HangoutResponse?> GetByIdAsync(HangoutIdRequest request)
        {
            var hangout = await _hangoutRepository.FindHangoutAsync(request.HangoutId);
            if (hangout == null) return null;
            return new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id);
        }

        public async Task<HangoutsResponse> GetAllHangoutsAsync()
        {
            var hangouts = await _hangoutRepository.GetAllHangoutsAsync();
            var responses = hangouts.Select(hangout => new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id)).ToList();
            return new HangoutsResponse(responses);
        }

        public async Task<HangoutsResponse> GetAllHangoutsByLovedOneIdAsync(LovedOneIdRequest request)
        {
            var hangouts = await _hangoutRepository.GetAllHangoutsByLovedOneIdAsync(request.LovedOneId);
            var responses = hangouts.Select(hangout => new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id)).ToList();
            return new HangoutsResponse(responses);
        }

        public async Task<OperationResult> DeleteHangoutAsync(HangoutIdRequest request)
        {
            var result = await _hangoutRepository.DeleteHangoutAsync(request.HangoutId);
            if (result == OperationResult.NotFound) return OperationResult.NotFound;
            return OperationResult.Deleted;
        }
    }

    public interface IHangoutService
    {
        Task<HangoutResponse?> CreateHangoutAsync(CreateHangoutRequest request);
        Task<HangoutResponse?> GetByIdAsync(HangoutIdRequest requst);
        Task<HangoutsResponse> GetAllHangoutsAsync();
        Task<HangoutsResponse> GetAllHangoutsByLovedOneIdAsync(LovedOneIdRequest request);
        Task<OperationResult> DeleteHangoutAsync(HangoutIdRequest request);
    }
}