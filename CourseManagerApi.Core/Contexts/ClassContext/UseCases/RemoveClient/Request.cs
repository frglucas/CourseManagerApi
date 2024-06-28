using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient;

public record Request(
    string ClassId,
    string ContractId
) : IRequest<Response>;