using BuberDinner.Domain.Common.Models;
using System.Reflection;

using BuberDinner.Domain.Menus;
using BuberDinner.Infrastructure.Context.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BuberDinner.Infrastructure.Context;

public class BuberDinnerDbContext : DbContext
{
    private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;
    public BuberDinnerDbContext(
        DbContextOptions options,
        PublishDomainEventInterceptor publishDomainEventInterceptor) : base(options)
    {
        _publishDomainEventInterceptor = publishDomainEventInterceptor
            ?? throw new ArgumentNullException(nameof(publishDomainEventInterceptor));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes()
           .SelectMany(e => e.GetProperties())
           .Where(p => p.IsPrimaryKey())
           .ToList()
           .ForEach(p => p.ValueGenerated = ValueGenerated.Never);

        modelBuilder
          .Ignore<List<IDomainEvent>>()
          .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Menu> Menus { get; set; } = default!;
}
