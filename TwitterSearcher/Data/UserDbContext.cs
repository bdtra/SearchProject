using TwitterSearcher.Models;
using Microsoft.EntityFrameworkCore;

namespace TwitterSearcher.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<UserSearch> UserSearches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSearch>()
                .HasKey(c => new { c.UserID, c.SearchID });
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        { }

    }
}
