using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Contracts;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient;

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

        Class? classObject;
        Client? client;
        Tenant? tenant;

        try
        {
            var tenantId = _contextAccessor.HttpContext.User.TenantId();
            tenant = await _repository.FindTenantByIdAsync(tenantId, cancellationToken);
            if (tenant == null)
                return new Response("Não foi possível encontrar o tenant", 404);

            classObject = await _repository.FindClassByIdAsync(request.ClassId, cancellationToken);
            if (classObject == null)
                return new Response("Não encontramos a turma informada", 404);

            client = await _repository.FindClientByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
                return new Response("Não encontramos o cliente informado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar clientes", 500);
        }

        #endregion

        #region 03. Validações
        
        try
        {
            if (!client.IsActive)
                return new Response("Não é possível adicionar clientes desativados", 400);

            var clintAlreadyRegisteredInClass = await _repository.AnyContractBetweenClientAndClassAsync(client.Id, classObject.Id, cancellationToken);
            if (clintAlreadyRegisteredInClass)
                return new Response("Cliente já cadastrado no curso", 400);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 500);
        }

        #endregion

        #region 04. Criar objetos

        Contract contract;

        try
        {
            contract = new Contract(classObject, client);
            contract.SetTenant(tenant);
        }
        catch
        {
            return new Response("Ocorreu um erro ao criar os contratos", 500);
        }

        #endregion

        #region 05. Persistir dados

        try
        {
            await _repository.SaveAsync(contract, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response("Cliente adicionado a turma", null);
    }
}