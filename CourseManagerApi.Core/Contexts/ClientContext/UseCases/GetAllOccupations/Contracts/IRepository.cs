using CourseManagerApi.Core.Contexts.ClientContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations.Contracts;

public interface IRepository
{
    Task<IEnumerable<Occupation>> GetAllOccupationsAsync(string term, CancellationToken cancellationToken);
}