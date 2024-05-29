using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.CourseContext.UseCases.GetAllByNameAndPaged;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<IEnumerable<Course>> GetAllByNameAsync(string term, CancellationToken cancellationToken) =>
        await _context
            .Courses
            .AsNoTracking()
            .Where(x => x.Name.Value.ToUpper().Contains(term.ToUpper()))
            .OrderBy(x => x.Name.Value)
            .ToListAsync(cancellationToken);
}