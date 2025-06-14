using Microsoft.EntityFrameworkCore;

namespace Immf.Models
{
    public class IMissMyFriendsDb : DbContext
    {
        public IMissMyFriendsDb(DbContextOptions options) : base(options) { }
        public DbSet<LovedOne> LovedOnes { get; set; } = null!;
        public DbSet<Hangout> Hangouts { get; set; } = null!;
    }
}