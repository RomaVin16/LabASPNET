using LabASPNET.Repository;
using Microsoft.EntityFrameworkCore; 

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RepositoryContext>(options =>
        options.UseNpgsql("Host=localhost;Port=5432;Database=ShopDB;Username=postgres;Password=1234"));

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");




    app.Run();
