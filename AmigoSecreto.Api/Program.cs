using AmigoSecreto.Api;
using AmigoSecreto.Application;
using AmigoSecreto.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure();


var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();