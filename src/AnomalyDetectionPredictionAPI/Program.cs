using AnomalyDetection.Common;
using AnomalyDetectionPredictionAPI.Services;
using Microsoft.OpenApi.Models;

// Configure app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Description = "Docs for my API", Version = "v1" });
});
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Define prediction route & handler
app.MapPost("/predict",
    async (ModelInput input) =>
        await Task.FromResult(PredictionEngineService.Predict(input)));

// Run app
app.Run();
