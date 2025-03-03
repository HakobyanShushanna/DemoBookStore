using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DemoBookStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoBookStoreContext") ?? throw new InvalidOperationException("Connection string 'DemoBookStoreContext' not found.")));

builder.Services.AddIdentity<Person, IdentityRole>().
    AddEntityFrameworkStores<DemoBookStoreContext>().
    AddDefaultTokenProviders();

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
