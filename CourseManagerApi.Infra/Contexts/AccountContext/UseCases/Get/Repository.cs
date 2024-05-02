using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.AccountContext.UseCases.Get;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
}