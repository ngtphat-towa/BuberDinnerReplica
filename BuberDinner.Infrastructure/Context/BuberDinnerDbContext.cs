using BuberDinner.Domain.Menus;

using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Context;

public class BuberDinnerDbContext : DbContext
{
    public BuberDinnerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected BuberDinnerDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Menu> Menus { get; set; } = default!;
}
