using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.Get.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Get;

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

        Course? course;

        try
        {
            course = await _repository.GetCourseByIdAsync(request.Id, cancellationToken);
            if (course is null)
                return new Response("Curso não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar curso", 500);
        }

        #endregion

        #region 03. Retorna os dados

        try
        {
            var data = new ResponseData(
                course.Id.ToString(),
                course.Name.Value,
                course.Description.Value,
                course.IsActive
            );

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados do curso", 500);
        }

        #endregion
    }
}