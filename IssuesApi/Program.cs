using IssuesApi.Classes.Context;
using IssuesApi.Repositories;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services;
using IssuesApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using IssuesApi.Domain.AutoMapperProfiles;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IssuesDbContext>(options =>
{
    options.UseSqlite("Data Source=testedb.sqlite");
});

builder.Services.AddAutoMapper(typeof(IssueItemProfile), typeof(ProjectProfile));

builder.Services.AddScoped<IIssuesRepository, IssuesRepository>();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

builder.Services.AddScoped<IIssuesService, IssuesService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions
        .Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
