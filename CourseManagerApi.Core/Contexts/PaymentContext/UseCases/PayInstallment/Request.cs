using MediatR;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment;

public record Request(string InstallmentId, string PaymentId) : IRequest<Response>;