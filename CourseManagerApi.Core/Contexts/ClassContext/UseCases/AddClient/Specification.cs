using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNull(request.ClientId, "ClientId", "Necessário informar um cliente")
            .IsNotNull(request.ClassId, "ClassId", "Necessário informar uma turma");
}