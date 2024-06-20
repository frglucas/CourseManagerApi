using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;
using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Core.Utils;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create;

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

        Occupation? occupation;
        Tenant? tenant;
        Lead? lead;
        User? creator;
        User? captivator;
        Client? indicator;

        try
        {
            if (!string.IsNullOrEmpty(request.LeadId)) {
                lead = await _repository.FindLeadByIdAsync(request.LeadId, cancellationToken);
                if (lead == null)
                    return new Response("Não encontramos o potencial cliente informado", 404);
                lead.SetIsAdhered(true);
            }

            if (request.OccupationId == null) occupation = null;
            else {
                occupation = await _repository.FindOccupationByIdAsync(request.OccupationId, cancellationToken);
                if (occupation == null)
                    return new Response("Não encontramos a ocupação profissional informada", 404);
            }

            var tenantId = _contextAccessor.HttpContext.User.TenantId();
            tenant = await _repository.FindTenantByIdAsync(tenantId, cancellationToken);
            if (tenant == null)
                return new Response("Não foi possível encontrar o tenant", 404);

            var creatorId = _contextAccessor.HttpContext.User.Id();
            creator = await _repository.FindCreatorByIdAsync(creatorId, cancellationToken);
            if (creator == null)
                return new Response("Não foi possível encontrar o criador", 404);

            captivator = await _repository.FindCaptivatorByIdAsync(request.CaptivatorId, cancellationToken);
            if (captivator == null)
                return new Response("Não foi possível encontrar o captador", 404);

            if (request.IndicatorIsCaptivator) indicator = null;
            else 
            {
                indicator = await _repository.FindIndicatorByIdAsync(request.IndicatorId, cancellationToken);
                if (indicator == null)
                    return new Response("Não foi possível encontrar o indicador", 404);
            }
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion
        
        #region 03. Gerar objetos
    
        Document document;
        Email email;
        Gender gender;
        Name name;
        Client client;

        try
        {
            document = new Document(StringUtils.FilterOnlyNumbers(request.Document), request.DocumentType);
            email = new Email(request.Email);
            gender = new Gender(request.GenderType, request.GenderDetail);
            
            if (string.IsNullOrEmpty(request.BadgeName) || string.IsNullOrWhiteSpace(request.BadgeName))
                name = new Name(request.FullName, request.FullName.Split(" ").First());
            else name = new Name(request.FullName, request.BadgeName);

            DateTime birthDate = DateTime.MinValue;

            if (request.DocumentType == Enums.EDocumentType.CPF)
                birthDate = request.BirthDate;

            client = new Client(email, name, document, gender, birthDate, request.Observation, request.IsSmoker, creator, captivator);
            if (indicator != null)
                client.SetIndicator(indicator);
            
            client.SetOccupation(occupation);
            client.SetTenant(tenant);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 04. Verifica se cliente existe no banco

        try
        {
            var documentExists = await _repository.AnyByDocumentAsync(document.HashedNumber, cancellationToken);
            if (documentExists)
                return new Response($"Este {document.Type.ToString()} já está em uso", 400);
            
            var emailExists = await _repository.AnyByEmailAsync(email.Address, cancellationToken);
            if (emailExists)
                return new Response("Este E-mail já está em uso", 400);
        }
        catch
        {
            return new Response("Falha ao verificar documento e email cadastrados", 500);
        }

        #endregion

        #region 05. Persiste os dados

        try
        {
            await _repository.SaveAsync(client, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response(
            "Cliente criado",
            new ResponseData(client.Id, client.Name, client.Email));
    }
}