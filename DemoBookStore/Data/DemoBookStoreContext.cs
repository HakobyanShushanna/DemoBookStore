using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Models;

namespace DemoBookStore.Data
{
    public class DemoBookStoreContext : DbContext
    {
        public DemoBookStoreContext (DbContextOptions<DemoBookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<DemoBookStore.Models.BookModel> BookModel { get; set; } = default!;
        public DbSet<DemoBookStore.Models.AuthorModel> AuthorModel { get; set; } = default!;
        public DbSet<DemoBookStore.Models.UserModel> UserModel { get; set; } = default!;
    }
}
