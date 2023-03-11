using Privatly.API;

var builder = WebApplication.CreateBuilder(args);
var serviceManager = new ServiceManager(builder.Configuration);
// Add services to the container.

serviceManager.Configure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();