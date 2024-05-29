using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<Course?> FindCourseByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Courses
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken: cancellationToken);

    public async Task<Tenant?> FindTenantByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken: cancellationToken);

    public async Task<User?> FindUserByIdAsync(string userId, CancellationToken cancellationToken) =>
        await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id.ToString() == userId, cancellationToken: cancellationToken);

    public async Task SaveAsync(Class entity, CancellationToken cancellationToken)
    {
        await _context.Classes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}