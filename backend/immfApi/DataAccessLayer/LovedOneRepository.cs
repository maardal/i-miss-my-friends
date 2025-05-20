using immfApi.Endpoints;
using immfApi.Models;
using Microsoft.EntityFrameworkCore;

namespace immfApi.DataAccessLayer
{
    public class LovedOneRepository : ILovedOneRepository
    {
        private readonly IMissMyFriendsDbContext _context;

        public LovedOneRepository(IMissMyFriendsDbContext context)
        {
            _context = context;
        }

        public async Task<LovedOne> AddAsync(LovedOne lovedOne)
        {
            await _context.LovedOnes.AddAsync(lovedOne);
            await _context.SaveChangesAsync();
            return lovedOne;
        }

        public async Task<LovedOne?> GetByIdAsync(int id)
        {
            return await _context.LovedOnes.Include(lovedOne => lovedOne.Hangouts).FirstOrDefaultAsync(lovedOne => lovedOne.Id == id);
        }

        public async Task<List<LovedOne>> GetAllAsync()
        {
            return await _context.LovedOnes.Include(lovedOne => lovedOne.Hangouts).ToListAsync();
        }


        public async Task<LovedOne?> UpdateAsync(int id, string name, Relationship relationship)
        {
            var lovedOne = await _context.LovedOnes.FindAsync(id);
            if (lovedOne == null) return null;
            lovedOne.Name = name;
            lovedOne.Relationship = relationship;
            await _context.SaveChangesAsync();
            return lovedOne;
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var lovedOne = await _context.LovedOnes.FindAsync(id);
            if (lovedOne == null) return OperationResult.NotFound;

            _context.LovedOnes.Remove(lovedOne);
            await _context.SaveChangesAsync();
            return OperationResult.Deleted;
        }
    }


    public interface ILovedOneRepository
    {
        Task<LovedOne> AddAsync(LovedOne lovedOne);
        Task<LovedOne?> GetByIdAsync(int id);
        Task<List<LovedOne>> GetAllAsync();
        Task<OperationResult> DeleteAsync(int id);
        Task<LovedOne?> UpdateAsync(int id, string name, Relationship relationship);
    }
}