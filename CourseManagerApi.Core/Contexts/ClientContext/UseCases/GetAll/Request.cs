using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll;

public record Request() : IRequest<Response>;