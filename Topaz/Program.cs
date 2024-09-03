using Microsoft.EntityFrameworkCore;
using Topaz.Models;

namespace Topaz;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string? con = builder.Configuration
            .GetConnectionString("Default");

        builder.Services.AddDbContext<ApplicationContext>
            (options => options.UseSqlServer(con));

        builder.Services.AddMvc();

        var app = builder.Build();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}