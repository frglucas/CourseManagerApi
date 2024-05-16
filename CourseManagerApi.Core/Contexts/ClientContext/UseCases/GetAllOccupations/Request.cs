using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations;

public record Request(string term) : IRequest<Response>;