using Microsoft.EntityFrameworkCore;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoBookStore.Data
{
    public class DemoBookStoreContext : IdentityDbContext<Person>
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
        public DbSet<AuthorModel> Authors { get; set; } = default!;
        public DbSet<UserModel> Users { get; set; } = default!;
        public DbSet<OrderModel> OrderModel { get; set; } = default!;
        public DbSet<BookModel> Books { get; set; } = default!;
    }
}
