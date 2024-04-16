using CourseManagerApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddHttpContextAcessor();
builder.AddDatabase();
builder.AddJwtAuthentication();
// builder.Services.AddSwaggerGen();

builder.AddAccountContext();
builder.AddClientContext();

builder.AddMediator();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();
app.MapClientEndpoints();

app.Run();
