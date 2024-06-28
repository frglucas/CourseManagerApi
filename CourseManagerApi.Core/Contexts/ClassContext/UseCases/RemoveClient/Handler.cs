using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient;

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

        Contract? contract;

        try
        {
            contract = await _repository.FindContractByIdAsync(request.ContractId, cancellationToken);
            if (contract == null)
                return new Response("Não encontramos o contrato informado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar os dados", 500);
        }

        #endregion

        #region 03. Validações
        
        try
        {
            if (contract.Class.Id.ToString().ToUpper() != request.ClassId.ToUpper())
                return new Response("Esse contrato não faz parte dessa turma", 400);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 500);
        }

        #endregion

        #region 04. Persistir dados

        try
        {
            await _repository.DeleteAsync(contract, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response("Cliente removido com sucesso", null);
    }
}