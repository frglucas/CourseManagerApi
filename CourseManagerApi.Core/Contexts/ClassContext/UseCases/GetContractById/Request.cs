using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById;

public record Request(string Id) : IRequest<Response>;