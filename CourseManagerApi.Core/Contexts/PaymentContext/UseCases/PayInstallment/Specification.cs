using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.InstallmentId, "InstallmentId", "Deve ser informado a parcela")
            .IsNotNullOrEmpty(request.PaymentId, "PaymentId", "Deve ser informado o pagamento");
}