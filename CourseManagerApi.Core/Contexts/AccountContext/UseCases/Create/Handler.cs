using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using CourseManagerApi.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
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
    
        #region 02. Gerar objetos

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion
    
        #region 03. Verifica se o usuário existe no banco

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este E-mail já está em uso", 400);
        }
        catch
        {
            return new Response("Falha ao verificar E-mail cadastrado", 500);
        }

        #endregion
    
        #region 04. Persiste os dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion
    
        #region 05. Envia email de ativação

        try
        {
            // TO DO
            //await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            // Do nothing
        }

        #endregion
    
        return new Response(
            "Conta criada",
            new ResponseData(user.Id, user.Name, user.Email));
    }
}