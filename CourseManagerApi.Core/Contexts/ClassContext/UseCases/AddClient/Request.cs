using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient;

public record Request(
    string ClientId,
    string ClassId
) : IRequest<Response>;