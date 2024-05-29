using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create;

public record Request(string CourseId, string MinisterId, string Name, string AddressOrLink, DateTime ScheduledDate, bool IsOnline) : IRequest<Response>;