using CommunicationModuleMonolith.API.BLL.Handlers;
using CommunicationModuleMonolith.API.BLL.Interfaces;
using CommunicationModuleMonolith.API.BLL.Services;
using CommunicationModuleMonolith.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services.AddScoped<IOperationHandler, CreateFileHandler>();
builder.Services.AddScoped<IOperationHandler, GetFileStatusHandler>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
