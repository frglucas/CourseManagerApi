using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged;

public record Request(string term, int page, int pageSize) : IRequest<Response>;