using CourseManagerApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddHttpContextAcessor();
builder.AddDatabase();
builder.AddJwtAuthentication();
// builder.Services.AddSwaggerGen();

builder.AddAccountContext();
builder.AddClientContext();
builder.AddCourseContext();
builder.AddClassContext();

builder.AddMediator();
builder.AddCorsPolicy();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseCors("CourseManagerCors");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();
app.MapClientEndpoints();
app.MapCourseEndpoints();
app.MapClassEndpoints();

app.Run();
