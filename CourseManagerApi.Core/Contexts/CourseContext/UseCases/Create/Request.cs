using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create;

public record Request(
    string Name,
    string Description
) : IRequest<Response>;