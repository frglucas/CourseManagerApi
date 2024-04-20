using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.CourseContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create;

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

        Tenant? tenant;

        try
        {
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

        Name name;
        Description description;
        Course course;

        try
        {
            name = new Name(request.Name);
            description = new Description(request.Description);

            course = new Course(name, description);
            course.SetTenant(tenant);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion
    
        #region 04. Verificar se já existe curso no banco

        try
        {            
            var nameExists = await _repository.AnyByNameAsync(course.Name.ToString(), cancellationToken);
            if (nameExists)
                return new Response("Este nome já está existe", 400);
        }
        catch
        {
            return new Response("Falha ao verificar documento e email cadastrados", 500);
        }

        #endregion
    
        #region 05. Persiste os dados

        try
        {
            await _repository.SaveAsync(course, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion
        
        return new Response("Curso criado", new ResponseData(course.Id, course.Name));
    }
}