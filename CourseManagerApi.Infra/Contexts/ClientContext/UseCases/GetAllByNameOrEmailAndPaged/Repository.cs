using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<IEnumerable<Client>> GetAllByNameOrEmailAsync(string term, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .Where(x => x.Name.Value.ToUpper().Contains(term.ToUpper()) || x.Email.Address.ToUpper().Contains(term.ToUpper()))
            .OrderBy(x => x.Name.Value)
            .ToListAsync(cancellationToken);
}