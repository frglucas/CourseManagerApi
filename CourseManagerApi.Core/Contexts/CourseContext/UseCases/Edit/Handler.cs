using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.UseCases.Edit.Contracts;
using CourseManagerApi.Core.Contexts.CourseContext.ValueObjects;
using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Edit;

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
            course = await _repository.FindCourseByIdAsync(request.CourseId, cancellationToken);  
            if (course == null)
                return new Response("Não encontramos o curso especificado", 404);
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Gerar objetos

        Name name;
        Description description;

        try
        {
            description = new Description(request.Description);
            name = new Name(request.Name);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 04. Validações
        
        // ANYTHING
        
        #endregion

        #region 05. Adiciona valores ao curso

        try
        {
            course.SetName(name);
            course.SetDescription(description);
            course.SetNewUpdateAt();
        }
        catch
        {
            return new Response("Erro ao inserir dados no curso", 500);
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
    
        return new Response("Curso editado", new ResponseData(course.Id));
    }
}