using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Infra.Contexts.AccountContext.Mappings;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Data;

public class CourseManagerDbContext : DbContext
{
    public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());

        modelBuilder.Ignore(typeof(Notification));
    }
}