using Microsoft.EntityFrameworkCore;
using icechat.api.Models;

namespace icechat.api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Groups> Groups { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<UserGroups> UserGroups { get; set; }

        public DbSet<Friends> Friends { get; set; }

        public DbSet<GroupMessages> GroupMessages { get; set; }

        public DbSet<PrivateMessages> PrivateMessages { get; set; }


    }
}
