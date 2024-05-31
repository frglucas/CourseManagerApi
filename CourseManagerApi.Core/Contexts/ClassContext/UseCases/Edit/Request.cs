using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit;

public record Request(string ClassId, string CourseId, string MinisterId, string Name, string AddressOrLink, DateTime ScheduledDate, bool IsOnline) : IRequest<Response>;