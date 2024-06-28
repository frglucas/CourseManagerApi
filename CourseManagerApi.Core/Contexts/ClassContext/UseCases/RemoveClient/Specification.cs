using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNull(request.ClassId, "ClassId", "Necessário informar uma turma")
            .IsNotNull(request.ContractId, "ContractId", "Necessário informar um contrato");
}