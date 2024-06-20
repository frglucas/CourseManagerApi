using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.GetAll;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<IEnumerable<Client>> GetAllClientsAsync(CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .OrderBy(x => x.Name.FullName)
            .ToListAsync(cancellationToken);
}