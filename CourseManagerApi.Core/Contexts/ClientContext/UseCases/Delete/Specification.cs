using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.Id, "Id", "Id inválido");
}