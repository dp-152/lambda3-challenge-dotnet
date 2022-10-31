using CopaGames.Application.External;
using CopaGames.Application.External.Interfaces;
using CopaGames.Application.Services;
using CopaGames.Application.Services.Interfaces;
using CopaGames.Application.Services.Validators;
using CopaGames.Infrastructure.Extensions.ServiceRegistration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}).AddNewtonsoftJson(opt =>
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

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(builder
                .Configuration
                .GetSection("CorsAllowedOrigins")
                .Get<string[]?>() ?? new[] { "*" })
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
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
app.UseExceptionHandler("/error");

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
