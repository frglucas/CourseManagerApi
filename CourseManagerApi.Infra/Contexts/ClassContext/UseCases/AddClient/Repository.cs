using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Contracts;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.AddClient;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<bool> AnyContractBetweenClientAndClassAsync(Guid clientId, Guid classId, CancellationToken cancellationToken) =>
        await _context
            .Contracts
            .Include(x => x.Client)
            .Include(x => x.Class)
            .AsNoTracking()
            .AnyAsync(x => x.Client.Id.ToString().ToUpper() == clientId.ToString().ToUpper() && x.Class.Id.ToString().ToUpper() == classId.ToString().ToUpper(), cancellationToken);
            

    public async Task<Class?> FindClassByIdAsync(string classId, CancellationToken cancellationToken) =>
        await _context
            .Classes
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == classId.ToUpper(), cancellationToken);

    public async Task<Client?> FindClientByIdAsync(string clientId, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == clientId.ToUpper(), cancellationToken);

    public async Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == tenantId.ToUpper(), cancellationToken);

    public async Task SaveAsync(Contract contract, CancellationToken cancellationToken)
    {
        await _context.Contracts.AddAsync(contract, cancellationToken);
    }
    
    public async Task SaveAsync(Payment payment, CancellationToken cancellationToken)
    {
        await _context.Payments.AddAsync(payment, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}