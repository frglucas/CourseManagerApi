using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged;

public record Request(string term, bool activeOnly, int page, int pageSize) : IRequest<Response>;