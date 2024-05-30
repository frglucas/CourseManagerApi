using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAll.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.CourseContext.UseCases.GetAll;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<IEnumerable<Course>> GetAll(CancellationToken cancellationToken) =>
        await _context
            .Courses
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
}