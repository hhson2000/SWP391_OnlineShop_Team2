using Microsoft.EntityFrameworkCore;
using NitStore.Models.Domain;

namespace NitStore.Data
{
    public class NitDbContext : DbContext
    {
        public NitDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<NitStore.Models.Domain.Category> Category { get; set; }

        public DbSet<NitStore.Models.Domain.Image> Image { get; set; }
        public DbSet<NitStore.Models.Domain.Product> Product { get; set; }

    }
}
