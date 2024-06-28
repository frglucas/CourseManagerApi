using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Recupera valores

        IEnumerable<Client> clients;
        try
        {
            clients = await _repository.GetAllClientsAsync(cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível recuperar os valores", 500);
        }

        #endregion

        #region 02. Retorna os dados

        try
        {
            var data = clients.Select(client => {
                return new ResponseData
                {
                    Id = client.Id.ToString(),
                    Name = client.Name,
                    Email = client.Email,
                    IsActive = client.IsActive
                };
            });

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados", 500);
        }

        #endregion
    }
}