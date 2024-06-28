using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.UseCases.RemoveInstallment.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.PaymentContext.UseCases.RemoveInstallment;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<Installment?> FindInstallmentByIdAsync(string installmentId, CancellationToken cancellationToken) =>
        await _context
            .Installments
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == installmentId.ToUpper(), cancellationToken);

    public async Task SaveChangesAsync(Installment installment, CancellationToken cancellationToken)
    {
        _context.Installments.Remove(installment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}