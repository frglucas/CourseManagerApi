using MediatR;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment;

public record Request(decimal Money, string ContractId, DateTime DueDate) : IRequest<Response>;