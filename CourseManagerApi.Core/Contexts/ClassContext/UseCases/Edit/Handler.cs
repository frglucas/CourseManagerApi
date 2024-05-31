using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Contracts;
using CourseManagerApi.Core.Contexts.ClassContext.ValueObjects;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit;

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
        Class? entity;

        try
        {
            course = await _repository.FindCourseByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
                return new Response("Não encontramos o curso informado", 404);

            string tenantId = _contextAccessor.HttpContext.User.TenantId();
            minister = await _repository.FindUserByIdAsync(request.MinisterId, tenantId, cancellationToken);
            if (minister == null)
                return new Response("Não encontramos o ministrante informado", 404);

            entity = await _repository.FindClassByIdAsync(request.ClassId, cancellationToken);
            if (entity == null)
                return new Response("Não encontramos a turma informada", 404);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Gerar objetos
    
        Name name;

        try
        {
            name = new Name(request.Name);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 04. Adiciona valores ao cliente

        try
        {
            entity.SetCourse(course);
            entity.SetMinister(minister);
            entity.SetName(name);
            entity.SetAddressOrLink(request.AddressOrLink);
            entity.SetScheduledDate(request.ScheduledDate);
            entity.SetIsOnline(request.IsOnline);
            entity.SetNewUpdateAt();
        }
        catch
        {
            return new Response("Erro ao inserir dados no cliente", 500);
        }

        #endregion

        #region 06. Persiste dados

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return new Response("Erro ao persistir dados", 500);
        }

        #endregion
    
        return new Response("Turma editada", new ResponseData(entity.Id));
    }
}