using CourseManagerApi.Core.Contexts.AccountContext.Entities;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll.Contracts;

public interface IRepository
{
    Task<IEnumerable<User>> GetAll(string tenantId, CancellationToken cancellationToken);
}