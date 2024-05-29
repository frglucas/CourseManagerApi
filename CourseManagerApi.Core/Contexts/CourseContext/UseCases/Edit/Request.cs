using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Edit;

public record Request(
    string CourseId,
    string Name, 
    string Description
) : IRequest<Response>;