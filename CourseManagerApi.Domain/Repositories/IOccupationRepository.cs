
using CourseManagerApi.Domain.Entities;

namespace CourseManagerApi.Domain.Repositories;

public interface IOccupationRepository
{
    Task<Occupation> FindByIdAsync(int occupationId);
}