using MediatR;

namespace CourseManagerApi.Core.Contexts.AccountContext.UseCases.Authenticate;

public record Request(string Email, string Password) : IRequest<Response>;