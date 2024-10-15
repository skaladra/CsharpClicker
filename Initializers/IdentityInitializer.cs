using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Web.Initializers;

public static class IdentityInitializer
{
    public static void InitializeIdentity(IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(options => options.Password.RequireNonAlphanumeric = false);
    }
}
