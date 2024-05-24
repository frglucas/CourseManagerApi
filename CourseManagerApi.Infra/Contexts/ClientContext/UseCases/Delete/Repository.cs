using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Delete;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<Client?> FindClientById(string id, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}