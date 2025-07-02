using Microsoft.EntityFrameworkCore;

namespace immfApi.Models
{
    public class IMissMyFriendsDbContext : DbContext
    {
        public IMissMyFriendsDbContext(DbContextOptions options) : base(options) { }
        public DbSet<LovedOne> LovedOnes { get; set; } = null!;
        public DbSet<Hangout> Hangouts { get; set; } = null!;
    }
}