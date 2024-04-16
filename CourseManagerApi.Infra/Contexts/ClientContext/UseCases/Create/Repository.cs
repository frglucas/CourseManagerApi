using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<bool> AnyByDocumentAsync(string hash, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .AnyAsync(x => x.Document.HashedNumber == hash, cancellationToken: cancellationToken);

    public async Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email, cancellationToken: cancellationToken);

    public async Task<Occupation?> FindOccupationById(string occupationId, CancellationToken cancellationToken) =>
        await _context
            .Occupations
            .FirstOrDefaultAsync(x => x.Id.ToString() == occupationId, cancellationToken: cancellationToken);

    public async Task<Tenant?> FindTenantById(string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id.ToString() == tenantId, cancellationToken: cancellationToken);

    public async Task SaveAsync(Client client, CancellationToken cancellationToken)
    {
        await _context.Clients.AddAsync(client, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}