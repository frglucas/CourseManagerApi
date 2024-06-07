using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.LeadContext.UseCases.Get;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Lead?> GetLeadByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Leads
            .AsNoTracking()
            .Include(x => x.Creator)
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);
}