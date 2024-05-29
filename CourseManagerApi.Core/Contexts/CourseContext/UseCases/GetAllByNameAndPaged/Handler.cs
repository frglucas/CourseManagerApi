using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged;

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
        
        IEnumerable<Course> courses;

        try
        {
            courses = await _repository.GetAllByNameAsync(request.term, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }
        
        #endregion

        #region 03. Filtra cursos

        IEnumerable<Course> filteredCourses;

        try
        {
            if (request.activeOnly)
                filteredCourses = courses.AsQueryable().Where(x => x.IsActive);
            else filteredCourses = courses;
        }
        catch
        {
            return new Response("Não foi possivel filtrar informações necessárias", 500);
        }

        #endregion
    
        #region 04. Adiciona paginação

        IEnumerable<ResponseData> results;
        decimal pageCount;
        try
        {
            pageCount = Math.Ceiling((decimal)filteredCourses.Count() / request.pageSize);
            var skipValue = (request.page - 1) * request.pageSize;

            results = filteredCourses.AsQueryable().Skip((int)skipValue).Take(request.pageSize).Select(x => new ResponseData(x.Id, x.Name, x.Description, x.IsActive));
        }
        catch
        {
            return new Response("Não foi possivel filtrar os valores", 500);
        }

        #endregion

        #region 05. Retorna valores

        return new Response("", results, (int)pageCount, request.page);

        #endregion
    }
}