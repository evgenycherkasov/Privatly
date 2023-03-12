using Privatly.API;
using Privatly.API.Presentation.RESTApiControllers.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var serviceManager = new ServiceManager(builder.Configuration);
// Add services to the container.

serviceManager.Configure(builder.Services);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Privatly.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();