using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass;

public record Request(string Id) : IRequest<Response>;