using MediatR;

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
    }
}