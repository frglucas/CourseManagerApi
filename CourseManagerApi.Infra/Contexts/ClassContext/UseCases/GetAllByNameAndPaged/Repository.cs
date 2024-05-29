using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.GetAllByNameAndPaged;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<IEnumerable<Class>> GetAllByNameAsync(string term, CancellationToken cancellationToken) =>
        await _context
            .Classes
            .AsNoTracking()
            .Include(x => x.Course)
            .Where(x => x.Name.Value.ToUpper().Contains(term.ToUpper()))
            .OrderBy(x => x.Name.Value)
            .ToListAsync(cancellationToken);
}