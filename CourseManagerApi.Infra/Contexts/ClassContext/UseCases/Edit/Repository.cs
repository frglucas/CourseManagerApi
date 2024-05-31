using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Contracts;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.Edit;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Class?> FindClassByIdAsync(string classId, CancellationToken cancellationToken) =>
        await _context
            .Classes
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == classId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<Course?> FindCourseByIdAsync(string courseId, CancellationToken cancellationToken) =>
        await _context
            .Courses
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == courseId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<User?> FindUserByIdAsync(string ministerId, string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Users
            .Where(x => x.TenantId.HasValue)
            .Where(x => x.TenantId.Value.ToString().ToUpper() == tenantId.ToUpper())
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == ministerId.ToUpper(), cancellationToken: cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}