using CourseManagerApi.Core.Contexts.LeadContext.Entities;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get.Contracts;

public interface IRepository
{
    Task<Lead?> GetLeadByIdAsync(string id, CancellationToken cancellationToken);
}