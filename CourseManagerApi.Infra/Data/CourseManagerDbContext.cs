using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Infra.Contexts.AccountContext.Mappings;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CourseManagerApi.Infra.Extensions;
using CourseManagerApi.Infra.Contexts.TenantContext.Mappings;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Infra.Contexts.ClientContext.Mappings;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Infra.Contexts.CourseContext.Mappings;
using CourseManagerApi.Infra.Contexts.ClassContext.Mappings;
using CourseManagerApi.Infra.Contexts.PaymentContext.Mappings;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;

namespace CourseManagerApi.Infra.Data;

public class CourseManagerDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        => _httpContextAccessor = httpContextAccessor;

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Occupation> Occupations { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Installment> Installments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new TenantMap());
        modelBuilder.ApplyConfiguration(new ClientMap());
        modelBuilder.ApplyConfiguration(new OccupationMap());
        modelBuilder.ApplyConfiguration(new AddressMap());
        modelBuilder.ApplyConfiguration(new PhoneNumberMap());
        modelBuilder.ApplyConfiguration(new CourseMap());
        modelBuilder.ApplyConfiguration(new ClassMap());
        modelBuilder.ApplyConfiguration(new ContractMap());
        modelBuilder.ApplyConfiguration(new PaymentMap());
        modelBuilder.ApplyConfiguration(new InstallmentMap());

        modelBuilder.Ignore(typeof(Notification));

        var tenantId = _httpContextAccessor.HttpContext?.User.TenantId();
        modelBuilder.Entity<Client>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId").ToString() == tenantId);
        modelBuilder.Entity<Course>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId").ToString() == tenantId);
        modelBuilder.Entity<Class>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId").ToString() == tenantId);
        modelBuilder.Entity<Contract>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId").ToString() == tenantId);
        modelBuilder.Entity<Payment>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId").ToString() == tenantId);
        modelBuilder.Entity<Installment>().HasQueryFilter(x => EF.Property<Guid>(x, "TenantId").ToString() == tenantId);
    }
}