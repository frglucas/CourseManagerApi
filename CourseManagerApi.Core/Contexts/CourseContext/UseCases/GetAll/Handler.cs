using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAll.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAll;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Recupera os valores

        IEnumerable<Course> courses;
        try
        {
            courses = await _repository.GetAll(cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível recuperar valores", 500);
        }

        #endregion

        #region 02. Filtra valores

        // ANYTHING

        #endregion

        #region 03. Retorna os dados

        try
        {
            return new Response(string.Empty, courses.Select(x => new ResponseData(x.Id, x.Name.Value)));
        }
        catch
        {
            return new Response("Não foi possível obter os dados", 500);
        }

        #endregion
    }
}