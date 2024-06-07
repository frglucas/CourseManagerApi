using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get;

public record Request(string Id) : IRequest<Response>;