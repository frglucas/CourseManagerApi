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

        #region Edit

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Edit.Repository>();

        #endregion

        #region GetAllOccupations

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.GetAllOccupations.Repository>();

        #endregion

        #region GetAllByNameOrEmailAndPaged

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Repository>();

        #endregion
        
        #region Get

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.Get.Repository>();

        #endregion

        #region GetAll

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClientContext.UseCases.GetAll.Repository>();

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

        app.MapDelete("api/v1/clients/{id}", async (
            [FromRoute] string id,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Response> handler) => 
        {
            var result = await handler.Handle(new(id), new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region Edit

        app.MapPut("api/v1/clients", async (
            CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region GetAllOccupations

        app.MapGet("api/v1/clients/occupations", async (
            [FromQuery] string term,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations.Response> handler) =>
        {
            var result = await handler.Handle(new(term), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion

        #region GetAllByNameOrEmailAndPaged

        app.MapGet("api/v1/clients/paged", async (
            [FromQuery] string term,
            [FromQuery] bool activeOnly,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Response> handler) =>
        {
            var result = await handler.Handle(new(term, activeOnly, page, pageSize), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion
        
        #region Get

        app.MapGet("api/v1/clients", async (
            [FromQuery] string id,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get.Response> handler) =>
        {
            var result = await handler.Handle(new(id), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion

        #region GetAll

        app.MapGet("api/v1/clients/all", async (
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll.Request,
                CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll.Response> handler) =>
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