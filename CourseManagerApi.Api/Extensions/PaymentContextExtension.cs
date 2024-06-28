using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        
        #region PayInstallment

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.PaymentContext.UseCases.PayInstallment.Repository>();

        #endregion
        
        #region RemoveInstallment

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.PaymentContext.UseCases.RemoveInstallment.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.PaymentContext.UseCases.RemoveInstallment.Repository>();

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
                ? Results.Created($"api/v1/payment/add-installments/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
        
        #region PayInstallment

        app.MapPut("api/v1/payment/pay-installment", async (
            CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment.Request,
                CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region RemoveInstallment

        app.MapDelete("api/v1/payment/remove-installment", async (
            [FromQuery] string paymentId,
            [FromQuery] string installmentId,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.PaymentContext.UseCases.RemoveInstallment.Request,
                CourseManagerApi.Core.Contexts.PaymentContext.UseCases.RemoveInstallment.Response> handler) => 
        {
            var result = await handler.Handle(new(installmentId, paymentId), new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
    }
}