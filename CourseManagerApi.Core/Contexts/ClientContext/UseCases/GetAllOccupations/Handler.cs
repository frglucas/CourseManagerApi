using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Recupera valores

        IEnumerable<Occupation> occupations;
        try
        {
            occupations = await _repository.GetAllOccupations(request.term, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível recuperar os valores", 500);
        }

        #endregion

        #region 02. Retorna os dados

        try
        {
            var data = occupations.Select(occupation => {
                return new ResponseData
                {
                    Id = occupation.Id.ToString(),
                    Description = occupation.Description
                };
            });

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados do perfil", 500);
        }

        #endregion
    }
}