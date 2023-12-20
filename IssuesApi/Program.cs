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
using IssuesApi.Classes.Hateoas;
using IssuesApi.Domain.Input;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Domain.Inputs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IssuesDbContext>(options =>
{
    options.UseSqlite("Data Source=testedb.sqlite");
});

builder.Services.AddAutoMapper(typeof(IssueItemProfile), typeof(ProjectProfile));

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureDependencyInjection();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

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
        Description = @"Simple issue tracking API I've built to
explore ease of interfacing with a Web API. 

To start you may register a user, using the v1/Users endpoint with the POST verb."
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

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(5001);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
Delegate d = () =>
{
    var res = new
    {
        Actions = new List<ApiAction>()
        {
            new()
            {
                Href = "http://localhost:5001/v1/Auth",
                Rel = "Login",
                Method = HttpMethod.Post.ToString(),
                Fields = DtoUtils.GetFields<LogInDTO>()
            },
            new()
            {
                Href = "http://localhost:5001/v1/Users",
                Rel = "Register",
                Method = HttpMethod.Post.ToString(),
                Fields = DtoUtils.GetFields<CreateUserDTO>()
            }
        }
    };

    return res;
};

app.MapGet("", d);

app.Run();
