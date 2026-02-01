using BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessIT_Prueba_Tecnica_Desarrollador_Fullstack.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Relation ID Clients - Services 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientService>()
                .HasKey(cs => new { cs.ClientId, cs.ServiceId });
            base.OnModelCreating(modelBuilder);

            // Global Filter IsActive for all BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsActive));
                    var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(true)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }

        // State and DateTime Control
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDatetime = DateTime.UtcNow;
                        entry.Entity.UpdateDatetime = DateTime.UtcNow;
                        entry.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDatetime = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsActive = false;
                        entry.Entity.UpdateDatetime = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ClientService> ClientServices { get; set; }
    }
}
