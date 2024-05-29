using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged;

public record Request(string term, int page, int pageSize) : IRequest<Response>;