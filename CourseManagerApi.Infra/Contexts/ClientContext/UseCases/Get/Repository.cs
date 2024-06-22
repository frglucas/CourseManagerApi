using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Get;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Client?> GetClientByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .Include(x => x.Occupation)
            .Include(x => x.Creator)
            .Include(x => x.Captivator)
            .Include(x => x.Indicator)
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);
}