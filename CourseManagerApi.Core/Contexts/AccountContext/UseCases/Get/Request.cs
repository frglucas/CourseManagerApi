using MediatR;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get;

public record Request() : IRequest<Response>;