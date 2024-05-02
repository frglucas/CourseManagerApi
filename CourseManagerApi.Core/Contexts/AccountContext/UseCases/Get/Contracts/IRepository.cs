using CourseManagerApi.Core.Contexts.AccountContext.Entities;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get.Contracts;

public interface IRepository
{
    Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
}