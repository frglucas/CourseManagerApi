using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagerApi.Api.Extensions;

public static class ClientContextExtensions
{
    public static void AddClientContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Create.Repository>();

        #endregion

        #region Delete

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Delete.Repository>();

        #endregion
    }

    public static void MapClientEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/clients", async (
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/clients/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region Delete

        app.MapDelete("api/v1/clients", async (
            [FromBody] CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
    }
}