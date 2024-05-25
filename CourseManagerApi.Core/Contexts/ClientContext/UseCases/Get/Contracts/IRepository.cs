using CourseManagerApi.Core.Contexts.ClientContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get.Contracts;

public interface IRepository
{
    Task<Client?> GetClientByIdAsync(string id, CancellationToken cancellationToken);
}