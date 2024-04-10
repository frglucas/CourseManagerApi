using CourseManagerApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
// builder.Services.AddSwaggerGen();

builder.AddAccountContext();

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

app.MapAccountEndpoints();

app.Run();
