using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.Id, "Id", "Id inválido");
}