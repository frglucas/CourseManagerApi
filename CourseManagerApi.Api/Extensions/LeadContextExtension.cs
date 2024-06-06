using MediatR;

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
    }
}