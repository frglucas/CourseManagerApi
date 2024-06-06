using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<IEnumerable<Lead>> GetAllByNameOrEmailAsync(string term, CancellationToken cancellationToken) =>
        await _context
            .Leads
            .AsNoTracking()
            .Where(x => x.IsAdhered == false)
            .Where(x => x.Name.Value.ToUpper().Contains(term.ToUpper()) || x.Email.Address.ToUpper().Contains(term.ToUpper()))
            .OrderBy(x => x.Name.Value)
            .ToListAsync(cancellationToken);
}