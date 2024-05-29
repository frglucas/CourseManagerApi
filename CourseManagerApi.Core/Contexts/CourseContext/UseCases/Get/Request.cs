using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Get;

public record Request(string Id) : IRequest<Response>;