using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get;

public record Request(string Id) : IRequest<Response>;