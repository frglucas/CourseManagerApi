using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using MediatR;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment;

public record Request(decimal Money, string ContractId, DateTime DueDate, EPaymentMethod PaymentMethod) : IRequest<Response>;