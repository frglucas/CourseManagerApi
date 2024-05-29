using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagerApi.Api.Extensions;

public static class CourseContextExtension
{
    public static void AddCourseContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.CourseContext.UseCases.Create.Repository>();

        #endregion

        #region Delete

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.CourseContext.UseCases.Delete.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.CourseContext.UseCases.Delete.Repository>();

        #endregion

        #region GetAllByNameAndPaged

        builder.Services.AddTransient<
            CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Contracts.IRepository,
            CourseManagerApi.Infra.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Repository>();

        #endregion
    }

    public static void MapCourseEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/courses", async (
            CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Request request,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Request,
                CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/courses/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region Delete

        app.MapDelete("api/v1/courses/{id}", async (
            [FromRoute] string id,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.CourseContext.UseCases.Delete.Request,
                CourseManagerApi.Core.Contexts.CourseContext.UseCases.Delete.Response> handler) => 
        {
            var result = await handler.Handle(new(id), new CancellationToken());
            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region GetAllByNameAndPaged

        app.MapGet("api/v1/courses/paged", async (
            [FromQuery] string term,
            [FromQuery] bool activeOnly,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            IRequestHandler<
                CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Request,
                CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Response> handler) => 
        {
            var result = await handler.Handle(new(term, activeOnly, page, pageSize), new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            return Results.Ok(result);
        }).RequireAuthorization();

        #endregion
    }
}