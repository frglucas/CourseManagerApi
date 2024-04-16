using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete;

public record Request(string Id) : IRequest<Response>;