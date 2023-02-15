using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Common.Models;
using SchoolManagementSystem.Domain.Entities;
using System.Reflection;

namespace SchoolManagementSystem.Persistence;

public class ApplicationDbContext : DbContext
{

    private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
       typeof(ApplicationDbContext).GetMethod(
           nameof(SetIsDeletedQueryFilter),
           BindingFlags.NonPublic | BindingFlags.Static);


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {

    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    public DbSet<ExamResult> ExamResults { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);



        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        EntityIndexesConfiguration.Configure(builder);

        var entityTypes = builder.Model.GetEntityTypes().ToList();

        // Set global query filter for not deleted entities only
        var deletableEntityTypes = entityTypes
            .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
            method.Invoke(null, new object[] { builder });
        }

        // Disable cascade delete
        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
        where T : class, IDeletableEntity
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }


    public override int SaveChanges() => this.SaveChanges(true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyAuditInfoRules();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        this.SaveChangesAsync(true, cancellationToken);

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        this.ApplyAuditInfoRules();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        return result;
    }


    private void ApplyAuditInfoRules()
    {
        var changedEntries = this.ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IAuditInfo &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in changedEntries)
        {
            var entity = (IAuditInfo)entry.Entity;
            if (entry.State == EntityState.Added && entity.CreatedOn == default)
            {
                entity.CreatedOn = DateTime.Now;
            }
            else
            {
                entity.ModifiedOn = DateTime.Now;
            }
        }
    }



}