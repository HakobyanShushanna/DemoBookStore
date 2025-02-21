using Microsoft.EntityFrameworkCore;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoBookStore.Data
{
    public class DemoBookStoreContext : IdentityDbContext<UserModel>
    {
        public DemoBookStoreContext (DbContextOptions<DemoBookStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().ToTable("AspNetUsers");
            modelBuilder.Entity<AuthorModel>().ToTable("Authors");
        }

        public DbSet<DemoBookStore.Models.BookModel> Books { get; set; } = default!;
        public DbSet<DemoBookStore.Models.AuthorModel> Authors { get; set; } = default!;
        public DbSet<DemoBookStore.Models.UserModel> Users { get; set; } = default!;
        public DbSet<Order> Orders { get; set; }
    }
}
