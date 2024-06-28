using MediatR;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.RemoveInstallment;

public record Request(string InstallmentId, string PaymentId) : IRequest<Response>;