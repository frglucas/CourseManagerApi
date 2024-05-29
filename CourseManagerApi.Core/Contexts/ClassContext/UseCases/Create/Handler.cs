using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.ClassContext.ValueObjects;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create;

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

        Course? course;
        User? minister;
        Tenant? tenant;

        try
        {
            course = await _repository.FindCourseByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
                return new Response("Não encontramos o curso informado", 404);

            var tenantId = _contextAccessor.HttpContext.User.TenantId();
            tenant = await _repository.FindTenantByIdAsync(tenantId, cancellationToken);
            if (tenant == null)
                return new Response("Não foi possível encontrar o tenant", 404);

            minister = await _repository.FindUserByIdAsync(request.MinisterId, cancellationToken);
            if (minister == null)
                return new Response("Não encontramos o ministrante informado", 404);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Gerar objetos
    
        Name name;
        Class entity;

        try
        {
            name = new Name(request.Name);

            entity = new Class(course, minister, name, request.AddressOrLink, request.ScheduledDate, request.IsOnline);
            entity.SetTenant(tenant);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 04. Verifica se cliente existe no banco

        // ANYTHING

        #endregion

        #region 05. Persiste os dados

        try
        {
            await _repository.SaveAsync(entity, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response("Turma criada", new ResponseData(entity.Id));
    }
}