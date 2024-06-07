using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagerApi.Api.Extensions;

public static class LeadContextExtensions
{
    public static void AddLeadContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.LeadContext.UseCases.Create.Repository>();

        #endregion

        #region GetAllByNameOrEmailAndPaged

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged.Repository>();

        #endregion
        
        #region Get

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.LeadContext.UseCases.Get.Repository>();

        #endregion
        
        #region Edit

        builder.Services.AddScoped<
            CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.LeadContext.UseCases.Edit.Repository>();

        #endregion
    }

    public static void MapLeadEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/leads", async (
            CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create.Request,
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/leads/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region GetAllByNameOrEmailAndPaged

        app.MapGet("api/v1/leads/paged", async (
            [FromQuery] string term,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged.Request,
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.GetAllByNameOrEmailAndPaged.Response> handler) =>
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

        app.MapGet("api/v1/leads", async (
            [FromQuery] string id,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get.Request,
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get.Response> handler) =>
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

        app.MapPut("api/v1/leads", async (
            CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Request,
                CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
    }
}