using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.LeadContext.Entities;
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

    public async Task<User?> FindCaptivatorByIdAsync(string captivatorId, CancellationToken cancellationToken) =>
        await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == captivatorId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<User?> FindCreatorByIdAsync(string creatorId, CancellationToken cancellationToken) =>
        await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == creatorId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<Client?> FindIndicatorByIdAsync(string indicatorId, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == indicatorId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<Lead?> FindLeadByIdAsync(string leadId, CancellationToken cancellationToken) => 
        await _context
            .Leads
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == leadId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<Occupation?> FindOccupationByIdAsync(string occupationId, CancellationToken cancellationToken) =>
        await _context
            .Occupations
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == occupationId.ToUpper(), cancellationToken: cancellationToken);

    public async Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == tenantId.ToUpper(), cancellationToken: cancellationToken);

    public async Task SaveAsync(Client client, CancellationToken cancellationToken)
    {
        await _context.Clients.AddAsync(client, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}