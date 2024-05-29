using CourseManagerApi.Core.Contexts.ClientContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Contracts;

public interface IRepository
{
    Task<bool> AnyByDocumentAsync(string hashedNumber, CancellationToken cancellationToken);
    Task<bool> AnyByEmailAsync(string address, CancellationToken cancellationToken);
    Task<Client?> FindClientByIdAsync(string clientId, CancellationToken cancellationToken);
    Task<Occupation?> FindOccupationByIdAsync(string occupationId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}