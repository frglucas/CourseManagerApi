using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

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

        Class? getClass;

        try
        {
            getClass = await _repository.GetClassByIdAsync(request.Id, cancellationToken);
            if (getClass is null)
                return new Response("Turma não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar turma", 500);
        }

        #endregion

        #region 03. Retorna os dados

        try
        {
            var data = new ResponseData(
                getClass.Id.ToString(),
                getClass.Name,
                getClass.Course.Id.ToString(),
                getClass.Minister.Id.ToString(),
                getClass.AddressOrLink,
                getClass.ScheduledDate,
                getClass.IsOnline
            );

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados da turma", 500);
        }

        #endregion
    }
}