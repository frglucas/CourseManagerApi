using CourseManagerApi.Core.Contexts.AccountContext.Entities;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}