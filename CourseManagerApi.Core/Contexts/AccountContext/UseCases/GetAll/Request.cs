using MediatR;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll;

public record Request() : IRequest<Response>;