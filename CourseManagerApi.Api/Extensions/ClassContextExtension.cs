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
        
        #region GetAllByNameAndPaged

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.GetAllByNameAndPaged.Repository>();

        #endregion
        
        #region Get

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.Get.Repository>();

        #endregion

        #region Edit

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.Edit.Repository>();

        #endregion

        #region AddClient

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.AddClient.Repository>();

        #endregion

        #region GetAllContractsByClass

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.GetAllContractsByClass.Repository>();

        #endregion
        
        #region RemoveClient

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.RemoveClient.Repository>();

        #endregion

        #region GetContractById

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.ClassContext.UseCases.GetContractById.Repository>();

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

        #region GetAllByNameOrEmailAndPaged

        app.MapGet("api/v1/classes/paged", async (
            [FromQuery] string term,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged.Response> handler) =>
        {
            var result = await handler.Handle(new(term, page, pageSize), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion

        #region Get

        app.MapGet("api/v1/classes", async (
            [FromQuery] string id,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.Get.Response> handler) =>
        {
            var result = await handler.Handle(new(id), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion

        #region Edit

        app.MapPut("api/v1/classes", async (
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region AddClient

        app.MapPost("api/v1/classes/add-client", async (
            CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region GetAllContractsByClass

        app.MapGet("api/v1/classes/contracts", async (
            [FromQuery] string classId,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass.Response> handler) =>
        {
            var result = await handler.Handle(new(classId), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion

        #region RemoveClient

        app.MapDelete("api/v1/classes/remove-client", async (
            [FromQuery] string classId,
            [FromQuery] string contractId,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient.Response> handler) => 
        {
            var result = await handler.Handle(new(classId, contractId), new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region GetContractById

        app.MapGet("api/v1/classes/contract", async (
            [FromQuery] string id,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById.Request,
                CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById.Response> handler) =>
        {
            var result = await handler.Handle(new(id), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion
    }
}