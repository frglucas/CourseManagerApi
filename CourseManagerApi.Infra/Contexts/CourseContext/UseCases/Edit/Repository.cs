using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.Edit.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.CourseContext.UseCases.Edit;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Course?> FindCourseByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Courses
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}