using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClassContext.UseCases.GetAllContractsByClass;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<List<Contract>> GetContractsByClassIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Contracts
            .Include(x => x.Client)
            .Include(x => x.Payment)
            .ThenInclude(x => x.Installments)
            .Where(x => x.Class.Id.ToString().ToUpper() == id.ToUpper())
            .ToListAsync(cancellationToken);
}