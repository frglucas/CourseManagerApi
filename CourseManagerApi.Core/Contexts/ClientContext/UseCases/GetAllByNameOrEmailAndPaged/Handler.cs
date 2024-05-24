using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valida a requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Recupera objetos
        
        IEnumerable<Client> clients;

        try
        {
            clients = await _repository.GetAllByNameOrEmailAsync(request.term, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }
        
        #endregion

        #region 03. Filtra clientes

        IEnumerable<Client> filteredClients;

        try
        {
            if (request.activeOnly)
                filteredClients = clients.AsQueryable().Where(x => x.IsActive);
            else filteredClients = clients;
        }
        catch
        {
            return new Response("Não foi possivel filtrar informações necessárias", 500);
        }

        #endregion

        #region 04. Adiciona paginação

        IEnumerable<ResponseData> results;
        decimal pageCount;
        try
        {
            pageCount = Math.Ceiling((decimal)filteredClients.Count() / request.pageSize);
            var skipValue = (request.page - 1) * request.pageSize;

            results = filteredClients.AsQueryable().Skip((int)skipValue).Take(request.pageSize).Select(x => new ResponseData(x.Id, x.Name, x.Email, x.IsActive));
        }
        catch
        {
            return new Response("Não foi possivel filtrar os valores", 500);
        }

        #endregion

        #region 05. Retorna valores

        return new Response("", results, (int)pageCount, request.page);

        #endregion
    }
}