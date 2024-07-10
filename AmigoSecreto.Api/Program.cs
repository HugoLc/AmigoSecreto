using AmigoSecreto.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPresentation();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();