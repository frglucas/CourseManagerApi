using MediatR;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged;

public record Request(string term, bool activeOnly, int page, int pageSize) : IRequest<Response>;