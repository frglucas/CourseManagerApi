using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get.Contracts;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get;

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
        #region 01. Valida requisição

        string? id;
        try
        {
            id = _contextAccessor.HttpContext?.User.Id();
            if (id == null)
                return new Response("Requisição inválida", 400);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Recupera o perfil

        User? user;
        try
        {
            user = await _repository.GetUserByIdAsync(id, cancellationToken);
            if (user is null)
                return new Response("Perfil não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar seu perfil", 500);
        }

        #endregion

        #region 03. Verifica se a conta está verificada

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Conta inativa", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion

        #region 04. Retorna os dados

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                TenantId = user.TenantId.ToString(),
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados do perfil", 500);
        }

        #endregion
    }
}