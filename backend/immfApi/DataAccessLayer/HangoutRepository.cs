using immfApi.Endpoints;
using immfApi.Models;
using Microsoft.EntityFrameworkCore;

namespace immfApi.DataAccessLayer
{
    public class HangoutRepository : IHangoutRepository
    {
        private readonly IMissMyFriendsDbContext _context;

        public HangoutRepository(IMissMyFriendsDbContext context)
        {
            _context = context;
        }

        public async Task<LovedOne?> FindLovedOneAsync(int id)
        {
            return await _context.LovedOnes.FindAsync(id);
        }

        public async Task<Hangout> CreateHangoutAsync(Hangout hangout)
        {
            await _context.Hangouts.AddAsync(hangout);
            await _context.SaveChangesAsync();
            return hangout;
        }

        public async Task<Hangout?> FindHangoutAsync(int id)
        {
            return await _context.Hangouts.Include(hangout => hangout.LovedOne).FirstOrDefaultAsync(hangout => hangout.Id == id);
        }

        public async Task<List<Hangout>> GetAllHangoutsAsync()
        {
            return await _context.Hangouts.Include(hangout => hangout.LovedOne).ToListAsync();
        }

        public async Task<List<Hangout>> GetAllHangoutsByLovedOneIdAsync(int id)
        {
            return await _context.Hangouts.Include(hangout => hangout.LovedOne).Where(hangout => hangout.LovedOne.Id == id).ToListAsync();
        }

        public async Task<OperationResult> DeleteHangoutAsync(int id)
        {
            var hangout = await _context.Hangouts.FindAsync(id);
            if (hangout == null) return OperationResult.NotFound;

            _context.Hangouts.Remove(hangout);
            await _context.SaveChangesAsync();
            return OperationResult.Deleted;

        }
    }
    public interface IHangoutRepository
    {
        Task<LovedOne?> FindLovedOneAsync(int id);
        Task<Hangout> CreateHangoutAsync(Hangout hangout);
        Task<Hangout?> FindHangoutAsync(int id);
        Task<List<Hangout>> GetAllHangoutsAsync();
        Task<List<Hangout>> GetAllHangoutsByLovedOneIdAsync(int id);
        Task<OperationResult> DeleteHangoutAsync(int id);
    }
}