using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.LeadContext.UseCases.Edit;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;
    
    public async Task<Lead?> FindLeadByIdAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Leads
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == id.ToUpper(), cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}