using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.CourseContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<bool> AnyByNameAsync(string name, CancellationToken cancellationToken) =>
        await _context
            .Courses
            .AsNoTracking()
            .AnyAsync(x => x.Name.Value == name, cancellationToken: cancellationToken);

    public async Task<Tenant?> FindTenantById(string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id.ToString() == tenantId, cancellationToken: cancellationToken);

    public async Task SaveAsync(Course course, CancellationToken cancellationToken)
    {
        await _context.Courses.AddAsync(course, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}