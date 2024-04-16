using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Contracts;

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
            client = await _repository.FindClientById(request.Id, cancellationToken);
            if (client == null)
                return new Response("Não encontramos o cliente informado", 404);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Verifica se cliente está ativo

        if (!client.IsActive)
            return new Response("Cliente já está desativado", 400);

        #endregion

        #region 04. Soft Delete

        try
        {
            client.Deactivate();
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 500);
        }

        #endregion

        #region 05. Persiste dados

        try
        {
            await _repository.SaveAsync(cancellationToken);
        }
        catch
        {
            return new Response("Erro ao salvar os dados", 500);
        }

        #endregion
    
        return new Response("Cliente desativado");
    }
}