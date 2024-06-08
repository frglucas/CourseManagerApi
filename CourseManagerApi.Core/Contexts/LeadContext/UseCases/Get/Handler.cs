using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get;

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

        Lead? lead;

        try
        {
            lead = await _repository.GetLeadByIdAsync(request.Id, cancellationToken);
            if (lead is null)
                return new Response("Cliente em potencia não encontrado", 404);
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
                lead.Id.ToString(),
                lead.Name,
                lead.Email,
                lead.PhoneNumber.AreaCode,
                lead.PhoneNumber.Number,
                lead.Observation,
                lead.Creator.Name,
                lead.IsAdhered
            );

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados", 500);
        }

        #endregion
    }
}