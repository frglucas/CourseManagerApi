using CourseManagerApi.Core;
using CourseManagerApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagerApi.Api.Extensions;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CourseManagerDbContext>(x => x.UseSqlServer(Configuration.Database.ConnectionString, b => b.MigrationsAssembly("CourseManagerApi.Api")));
    }

    public static void AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
    }
}