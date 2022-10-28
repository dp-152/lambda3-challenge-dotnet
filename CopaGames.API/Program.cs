using CopaGames.Application.External;
using CopaGames.Application.External.Interfaces;
using CopaGames.Application.Services;
using CopaGames.Application.Services.Interfaces;
using CopaGames.Application.Services.Validators;
using CopaGames.Infrastructure.Extensions.ServiceRegistration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.Formatting = Formatting.None;
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    opt.SerializerSettings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.RegisterHttpClients(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<TournamentRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddTransient<IGamesApi, ExternalGamesApi>();
builder.Services.AddTransient<IGameService, GameService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
