using CourseManagerApi.Core.Contexts.ClassContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get.Contracts;

public interface IRepository
{
    Task<Class?> GetClassByIdAsync(string id, CancellationToken cancellationToken);
}