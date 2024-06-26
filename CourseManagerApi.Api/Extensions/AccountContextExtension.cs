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

        #region Authenticate

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository>();

        #endregion

        #region Get

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.AccountContext.UseCases.Get.Repository>();

        #endregion

        #region GetAll

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.AccountContext.UseCases.GetAll.Repository>();

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

        #region Authenticate

        app.MapPost("api/v1/authenticate", async (
            CourseManagerApi.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            result.Data.Token = JwtExtension.Generate(result.Data);
            return Results.Ok(result);
        });

        #endregion

        #region Get

        app.MapGet("api/v1/user", async (
            IRequestHandler<
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get.Request,
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.Get.Response> handler) =>
        {
            var result = await handler.Handle(new(), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion

        #region GetAll

        app.MapGet("api/v1/user/all", async (
            IRequestHandler<
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll.Request,
                CourseManagerApi.Core.Contexts.AccountContext.UseCases.GetAll.Response> handler) =>
        {
            var result = await handler.Handle(new(), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion
    }
}