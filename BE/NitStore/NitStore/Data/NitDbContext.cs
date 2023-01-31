using Microsoft.EntityFrameworkCore;
using NitStore.Models.Domain;

namespace NitStore.Data
{
    public class NitDbContext : DbContext
    {
        public NitDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> users { get; set; }
    }
}
