using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get;

public record Request(string Id) : IRequest<Response>;