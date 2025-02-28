using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entities = ChangeTracker.Entries<Entity>().ToList();

        entities.Where(x => x.State == EntityState.Added).ToList().ForEach(x =>
         {
             x.Entity.CreatedDate = DateTime.Now;
         });

        entities.Where(x => x.State == EntityState.Modified).ToList().ForEach(x =>
        {
            x.Entity.UpdatedDate = DateTime.Now;
        });
        return base.SaveChangesAsync(cancellationToken);
    }

    //AppDbContext context = new AppDbContext();
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=.;Database=CleanArchitecture;Trusted_Connection=True;");
    //    //base.OnConfiguring(optionsBuilder);
    //}
}