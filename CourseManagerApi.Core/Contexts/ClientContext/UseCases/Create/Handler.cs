using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
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

        try
        {
            occupation = await _repository.FindOccupationById(request.OccupationId, cancellationToken);
            if (occupation == null)
                return new Response("Não encotramos a ocupação profissional informada", 404);

            var tenantId = _contextAccessor.HttpContext.User.TenantId();
            tenant = await _repository.FindTenantById(tenantId, cancellationToken);
            if (tenant == null)
                return new Response("Não foi possível encontrar o tenant", 404);
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
        AccountContext.ValueObjects.Name name;
        Client client;

        try
        {
            document = new Document(request.Document, request.DocumentType);
            email = new Email(request.Email);
            gender = new Gender(request.GenderType, request.GenderDetail);
            name = new AccountContext.ValueObjects.Name(request.Name);

            client = new Client(email, name, document, gender, request.BirthDate, occupation, request.Observation, request.IsSmoker);
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