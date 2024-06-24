using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Contracts;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.PaymentContext.UseCases.AddInstallment;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Contract?> FindContractByIdAsync(string contractId, CancellationToken cancellationToken) =>
        await _context
            .Contracts
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == contractId.ToUpper(), cancellationToken);

    public async Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Tenants
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == tenantId.ToUpper(), cancellationToken);

    public async Task SaveAsync(Payment payment, CancellationToken cancellationToken)
    {
        await _context.Payments.AddAsync(payment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAsync(Installment installment, CancellationToken cancellationToken)
    {
        await _context.Installments.AddAsync(installment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}