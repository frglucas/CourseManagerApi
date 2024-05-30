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
    }
}