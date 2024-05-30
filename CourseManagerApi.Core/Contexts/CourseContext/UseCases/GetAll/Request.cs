using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAll;

public record Request() : IRequest<Response>;