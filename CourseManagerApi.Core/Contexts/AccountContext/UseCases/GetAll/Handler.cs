using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll.Contracts;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll;

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
        #region 01. Recupera os valores

        IEnumerable<User> users;
        try
        {
            string tenantId = _contextAccessor.HttpContext.User.TenantId();
            users = await _repository.GetAll(tenantId, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível recuperar valores", 500);
        }

        #endregion

        #region 02. Filtra contas verificadas

        IEnumerable<User> filteredUsers;
        try
        {
            filteredUsers = users.AsQueryable().Where(x => x.Email.Verification.IsActive);
        }
        catch
        {
            return new Response("Não foi possível filtrar valores", 500);
        }

        #endregion

        #region 04. Retorna os dados

        try
        {
            return new Response(string.Empty, filteredUsers.Select(x => new ResponseData(x.Id, x.Name.Value, x.Email.Address)));
        }
        catch
        {
            return new Response("Não foi possível obter os dados", 500);
        }

        #endregion
    }
}