using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.GetAllOccupations;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<IEnumerable<Occupation>> GetAllOccupationsAsync(string term, CancellationToken cancellationToken) => 
        await _context
            .Occupations
            .AsNoTracking()
            .Where(x => x.Description.ToUpper().Contains(term.ToUpper()))
            .OrderBy(x => x.Description)
            .ToListAsync(cancellationToken);
}