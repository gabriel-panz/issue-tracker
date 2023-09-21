using IssuesApi.Classes.Context;
using Microsoft.EntityFrameworkCore;
using IssuesApi.Domain.AutoMapperProfiles;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IssuesApi.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IssuesApi.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IssuesDbContext>(options =>
{
    options.UseSqlite("Data Source=testedb.sqlite");
});

builder.Services.AddAutoMapper(typeof(IssueItemProfile), typeof(ProjectProfile));

builder.Services.ConfigureDependencyInjection();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions
        .Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters.IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthService.Secret));

    options.TokenValidationParameters.ValidateIssuerSigningKey = false;

    options.TokenValidationParameters.ValidateLifetime = true;

    options.TokenValidationParameters.ValidateIssuer = false;

    options.TokenValidationParameters.ValidateAudience = false;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Issue Tracking Api",
        Version = "v1",
    });

    OpenApiSecurityScheme securityScheme = new()
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Name = "Authorization",
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey,
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new()
    {
        [new()
        {
            In = ParameterLocation.Header,
            Name = "Bearer",
            BearerFormat = "Bearer",
            Reference = new()
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme,
            },
            Scheme = "oauth2",
        }] = new List<string>(),
    });

    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
