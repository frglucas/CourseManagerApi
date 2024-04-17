using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Contracts;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Edit;

public class Repository : IRepository
{
    private readonly CourseManagerDbContext _context;

    public Repository(CourseManagerDbContext context) => _context = context;

    public async Task<bool> AnyByDocumentAsync(string hashedNumber, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .AnyAsync(x => x.Document.HashedNumber == hashedNumber, cancellationToken: cancellationToken);

    public async Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email, cancellationToken: cancellationToken);

    public async Task<Client?> FindClientById(string clientId, CancellationToken cancellationToken) =>
        await _context
            .Clients
            .FirstOrDefaultAsync(x => x.Id.ToString().ToUpper() == clientId.ToUpper(), cancellationToken);

    public async Task<Occupation?> FindOccupationById(string occupationId, CancellationToken cancellationToken) =>
        await _context
            .Occupations
            .FirstOrDefaultAsync(x => x.Id.ToString() == occupationId, cancellationToken: cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}