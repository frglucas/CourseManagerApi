using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.AccountContext.UseCases.GetAll;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<IEnumerable<User>> GetAll(string tenantId, CancellationToken cancellationToken) =>
        await _context
            .Users
            .AsNoTracking()
            .Where(x => x.TenantId.HasValue)
            .Where(x => x.TenantId.Value.ToString().ToUpper() == tenantId.ToUpper())
            .ToListAsync(cancellationToken);
}