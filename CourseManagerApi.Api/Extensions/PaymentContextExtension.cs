using MediatR;

namespace CourseManagerApi.Api.Extensions;

public static class PaymentContextExtensions
{
    public static void AddPaymentContext(this WebApplicationBuilder builder)
    {
        #region AddInstallment

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.PaymentContext.UseCases.AddInstallment.Repository>();

        #endregion
    }

    public static void MapPaymentEndpoints(this WebApplication app)
    {
        #region AddInstallment

        app.MapPost("api/v1/payment/add-installment", async (
            CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Request,
                CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/clients/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
    }
}