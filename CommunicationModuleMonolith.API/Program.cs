using CommunicationModuleMonolith.Services;
using CommunicationModuleMonolith.Interfaces;
using CommunicationModuleMonolith.Handlers;
using CommunicationModuleMonolith.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services.AddScoped<IOperationHandler, CreateFileHandler>();
builder.Services.AddScoped<IOperationHandler, GetFileStatusHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
