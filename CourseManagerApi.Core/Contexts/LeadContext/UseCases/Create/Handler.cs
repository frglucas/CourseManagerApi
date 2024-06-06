using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.LeadContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Core.Utils;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IHttpContextAccessor _contextAccessor;

    public Handler(IRepository repository, IHttpContextAccessor contextAccessor)
    {
        _repository = repository;
        _contextAccessor = contextAccessor;
    }
    
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

        User? creator;
        Tenant? tenant;

        try
        {
            var creatorId = _contextAccessor.HttpContext.User.Id();
            creator = await _repository.FindCreatorByIdAsync(creatorId, cancellationToken);
            if (creator == null)
                return new Response("Não foi possível encontrar o creator", 404);

            var tenantId = _contextAccessor.HttpContext.User.TenantId();
            tenant = await _repository.FindTenantByIdAsync(tenantId, cancellationToken);
            if (tenant == null)
                return new Response("Não foi possível encontrar o tenant", 404);
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
        Lead lead;

        try
        {
            phoneNumber = new PhoneNumber(StringUtils.FilterOnlyNumbers(request.AreaCode), StringUtils.FilterOnlyNumbers(request.PhoneNumber));
            email = new Email(request.Email);
            name = new Name(request.FullName);

            lead = new Lead(name, email, phoneNumber, request.Observation);
            lead.SetCreator(creator);
            lead.SetTenant(tenant);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 04. Persiste os dados

        try
        {
            await _repository.SaveAsync(lead, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response(
            "Cliente em potencial criado",
            new ResponseData(lead.Id, lead.Name, lead.Email));
    }
}