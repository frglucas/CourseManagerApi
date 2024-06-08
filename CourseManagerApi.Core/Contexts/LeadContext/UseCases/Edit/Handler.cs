using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Contracts;
using CourseManagerApi.Core.Contexts.LeadContext.ValueObjects;
using CourseManagerApi.Core.Utils;
using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit;

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
            lead = await _repository.FindLeadByIdAsync(request.LeadId, cancellationToken);
            if (lead == null)
                return new Response("Não foi possível encontrar o creator", 404);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Gerar objetos
    
        PhoneNumber phoneNumber;
        Email email;
        Name name;

        try
        {
            if (string.IsNullOrEmpty(request.AreaCode) || string.IsNullOrEmpty(request.PhoneNumber)) phoneNumber = new();
            else phoneNumber = new PhoneNumber(request.AreaCode, request.PhoneNumber);
            
            if (string.IsNullOrEmpty(request.Email)) email = new();
            else email = new Email(request.Email);
            
            name = new Name(request.FullName);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 04. Adiciona valores ao cliente em potencial

        try
        {
            lead.SetEmail(email);
            lead.SetName(name);
            lead.SetPhoneNumber(phoneNumber);
            lead.SetObservation(request.Observation);
            lead.SetNewUpdateAt();
        }
        catch
        {
            return new Response("Erro ao inserir dados no cliente em potencial", 500);
        }

        #endregion

        #region 05. Persiste dados

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return new Response("Erro ao persistir dados", 500);
        }

        #endregion
    
        return new Response("Cliente em potencial editado", new ResponseData(lead.Id));
    }
}