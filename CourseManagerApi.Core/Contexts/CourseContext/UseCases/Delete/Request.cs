using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Delete;

public record Request(string Id) : IRequest<Response>;