using SmartPPT.Template.Infrastructure.DependencyInjection;
using SmartPPT.SlideEngine.Infrastructure.DependencyInjection;
using SmartPPT.Agent.Infrastructure.DependencyInjection;
using SmartPPT.Presentation.Infrastructure.DependencyInjection;
using SmartPPT.Storage.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTemplateModule();
builder.Services.AddSlideEngineModule();
builder.Services.AddAgentModule();
builder.Services.AddPresentationModule();
builder.Services.AddStorageModule();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
