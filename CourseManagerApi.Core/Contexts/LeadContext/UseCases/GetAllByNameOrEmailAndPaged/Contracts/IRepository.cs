using CourseManagerApi.Core.Contexts.LeadContext.Entities;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts;

public interface IRepository
{
    Task<IEnumerable<Lead>> GetAllByNameOrEmailAsync(string term, CancellationToken cancellationToken);
}