using Microsoft.EntityFrameworkCore;
using RopaAmx.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RopaAmxContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RopaAmxConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// /Categoria/hombre, /Categoria/mujer, /Categoria/ninos
app.MapControllerRoute(
    name: "categoria",
    pattern: "Categoria/{slug}",
    defaults: new { controller = "Categoria", action = "Ver" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();