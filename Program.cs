using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.Infrastructure.DataAccess;
using CSharpClicker.Web.Initializers;
using Microsoft.OpenApi.Models;

namespace CSharpClicker.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapGet("/", () => "Hello World!");
        app.MapHealthChecks("health-check");

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();

        IdentityInitializer.InitializeIdentity(services);
        DbContextInitializer.InitializeDbContext(services);

        services.AddSwaggerGen();

        services.AddHealthChecks();
        services.AddControllersWithViews();
    }
}
