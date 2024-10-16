using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.Infrastructure.DataAccess;
using CSharpClicker.Web.Initializers;

namespace CSharpClicker.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        DbContextInitializer.InitializeDbContext(appDbContext);

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapGet("/", () => "Hello World!");
        app.MapHealthChecks("health-check");

        app.MapControllers();

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();

        DbContextInitializer.AddDbContext(services);
        IdentityInitializer.InitializeIdentity(services);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
        services.AddSwaggerGen();

        services.AddHealthChecks();
        services.AddControllersWithViews();
        services.AddAuthentication();
    }
}
