using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.GetContractById;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<Contract?> GetContractByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Contracts
            .Include(x => x.Client)
            .Include(x => x.Payment)
            .ThenInclude(x => x.Installments.OrderBy(x => x.DueDate))
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);
}