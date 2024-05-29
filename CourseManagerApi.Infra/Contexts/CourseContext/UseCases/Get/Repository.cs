using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.Get.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.CourseContext.UseCases.Get;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Course?> GetCourseByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);
}