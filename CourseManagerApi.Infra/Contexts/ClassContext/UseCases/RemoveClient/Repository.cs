using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.RemoveClient;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task DeleteAsync(Contract contract, CancellationToken cancellationToken) 
    {
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
    }

    public async Task<Contract?> FindContractByIdAsync(string contractId, CancellationToken cancellationToken) =>
        await _context
            .Contracts
            .Include(x => x.Class)
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == contractId.ToUpper(), cancellationToken);
}