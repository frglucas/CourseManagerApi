using CourseManagerApi.Core.Contexts.CourseContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Get.Contracts;

public interface IRepository
{
    Task<Course?> GetCourseByIdAsync(string id, CancellationToken cancellationToken);
}