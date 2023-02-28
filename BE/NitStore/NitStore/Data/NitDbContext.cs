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

        public DbSet<Image> images { get; set; }

<<<<<<< HEAD
        public DbSet<NitStore.Models.Domain.Image> Image { get; set; }
        public DbSet<NitStore.Models.Domain.Product> Product { get; set; }
=======
        public DbSet<Campaign> campaigns { get; set; }
>>>>>>> 609d9e599bed303c59c07f7bb8e9623d431446d8

        public DbSet<CampaignItem> campaignItems { get; set; }

        public DbSet<Feedback> feedbacks { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<Product> products { get;set; }

        public DbSet<OrderDetail> ordersDetail { get; set; }

        public DbSet<ProductImage> productsImage { get; set; }

        public DbSet<Slider> slider { get; set; }

        public DbSet<UserDetail> userDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImage>().HasNoKey();
        }
    }
}
