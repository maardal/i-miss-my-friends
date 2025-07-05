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

        public async Task<HangoutResponse?> GetByIdAsync(int hangoutId)
        {
            var hangout = await _hangoutRepository.FindHangoutAsync(hangoutId);
            if (hangout == null) return null;
            return new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id);
        }

        public async Task<HangoutsResponse> GetAllHangoutsAsync()
        {
            var hangouts = await _hangoutRepository.GetAllHangoutsAsync();
            var responses = hangouts.Select(hangout => new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id)).ToList();
            return new HangoutsResponse(responses);
        }

        public async Task<HangoutsResponse> GetAllHangoutsByLovedOneIdAsync(int lovedOneId)
        {
            var hangouts = await _hangoutRepository.GetAllHangoutsByLovedOneIdAsync(lovedOneId);
            var responses = hangouts.Select(hangout => new HangoutResponse(hangout.Id, hangout.Date, hangout.LovedOne.Id)).ToList();
            return new HangoutsResponse(responses);
        }

        public async Task<OperationResult> DeleteHangoutAsync(int hangoutId)
        {
            var result = await _hangoutRepository.DeleteHangoutAsync(hangoutId);
            if (result == OperationResult.NotFound) return OperationResult.NotFound;
            return OperationResult.Deleted;
        }
    }

    public interface IHangoutService
    {
        Task<HangoutResponse?> CreateHangoutAsync(CreateHangoutRequest request);
        Task<HangoutResponse?> GetByIdAsync(int hangoutId);
        Task<HangoutsResponse> GetAllHangoutsAsync();
        Task<HangoutsResponse> GetAllHangoutsByLovedOneIdAsync(int lovedOneId);
        Task<OperationResult> DeleteHangoutAsync(int hangoutId);
    }
}