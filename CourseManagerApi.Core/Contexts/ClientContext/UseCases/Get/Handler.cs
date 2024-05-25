using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get;

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

        Client? client;

        try
        {
            client = await _repository.GetClientByIdAsync(request.Id, cancellationToken);
            if (client is null)
                return new Response("Cliente não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar cliente", 500);
        }

        #endregion

        #region 03. Retorna os dados

        try
        {
            var data = new ResponseData(
                client.Id.ToString(),
                client.Name.FullName,
                client.Name.BadgeName,
                client.Email,
                client.BirthDate,
                client.Document.Type,
                client.Document.Number,
                client.Occupation.Id.ToString(),
                client.Gender.Type,
                client.Gender.Detail,
                client.IsSmoker,
                client.IsActive,
                client.Observation
            );

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados do perfil", 500);
        }

        #endregion
    }
}