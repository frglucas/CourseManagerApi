using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Infra.Contexts.AccountContext.Mappings;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CourseManagerApi.Infra.Extensions;
using CourseManagerApi.Infra.Contexts.TenantContext.Mappings;

namespace CourseManagerApi.Infra.Data;

public class CourseManagerDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        => _httpContextAccessor = httpContextAccessor;

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new TenantMap());

        modelBuilder.Ignore(typeof(Notification));

        var tenantId = _httpContextAccessor.HttpContext?.User.TenantId();
        // modelBuilder.Entity<T>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId") == tenantId);
    }
}