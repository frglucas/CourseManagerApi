using CourseManagerApi.Domain.Entities;

namespace CourseManagerApi.Domain.Repositories;

public interface IClientRepository
{
    Task CreateAsync(Client client);
    Task<bool> DocumentExistsAsync(string document);
    Task<bool> EmailExistsAsync(string email);
}