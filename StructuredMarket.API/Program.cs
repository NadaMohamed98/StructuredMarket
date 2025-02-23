using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Common;
using StructuredMarket.Infrastructure.Data;
using System.Text;
using Microsoft.OpenApi.Models;
using StructuredMarket.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var dbSettingsSection = configuration.GetSection("ConnectionStrings");
var dbSettings = dbSettingsSection.Get<ConnectionStrings>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StructuredMarket.Application.Features.Users.Queries.GetUserByIdQuery).Assembly));

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(dbSettings);

// Load configuration for Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// JWT
var jwtSettings = new JwtTokenSettings();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true
        };
    });

// Configure Swagger to support JWT authentication
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "StructuredMarket API", Version = "v1" });

    // Add JWT Authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the JWT token like this: Bearer {your_token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            new List<string>()
        }
    });
});

// Add Authorization
builder.Services.AddAuthorization();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Adds API versions in response headers
    options.AssumeDefaultVersionWhenUnspecified = true; // Uses default if no version is specified
    options.DefaultApiVersion = new ApiVersion(1, 0); // Sets default API version to 1.0
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Reads version from URL segment
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add custom response middleware
app.UseMiddleware<ResponseMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
