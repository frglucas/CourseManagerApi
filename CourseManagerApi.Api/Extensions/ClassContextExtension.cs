using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagerApi.Api.Extensions;

public static class ClassContextExtensions
{
    public static void AddClassContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.Create.Repository>();

        #endregion
    }

    public static void MapClassEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/classes", async (
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/clients/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
    }
}