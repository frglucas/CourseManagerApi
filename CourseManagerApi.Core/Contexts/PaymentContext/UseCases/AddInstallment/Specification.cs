using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNull(request.DueDate, "DueDate", "Deve ser informada um data de vencimento")
            .IsGreaterThan((int)request.PaymentMethod, 0, "PaymentMethod", "Deve ser informada um método de pagamento")
            .IsGreaterThan(request.Money, 0, "Money", "O valor da parcela informado deve ser maior que 0")
            .IsNotNullOrEmpty(request.ContractId, "ContractId", "Deve ser informado o contrato a ser adicionada a parcela");
}