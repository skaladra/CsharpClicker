using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Infrastructure.DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IAppDbContext
{
    public DbSet<ApplicationRole> ApplicationRoles { get; private set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; private set; }

    public DbSet<Boost> Boosts { get; private set; }

    public DbSet<UserBoost> UserBoosts { get; private set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserBoost>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBoosts)
            .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserBoost>()
            .HasOne(ub => ub.Boost)
            .WithMany()
            .HasForeignKey(ub => ub.BoostId);
    }
}
