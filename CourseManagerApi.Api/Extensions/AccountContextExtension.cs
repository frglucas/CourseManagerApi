using MediatR;

namespace CourseManagerApi.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.AccountContext.UseCases.Create.Repository>();

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/users", async (
            CourseManagerApi.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.Create.Request,
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.Create.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion
    }
}