using CourseManagerApi.Core.Contexts.LeadContext.Entities;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Contracts;

public interface IRepository
{
    Task<Lead?> FindLeadByIdAsync(string id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}