using newProject.Helpers;
using newProject.Entities;
using Microsoft.EntityFrameworkCore;
using newProject.Services;
using System.Text.Json.Serialization;
using AutoMapper;
using System.Reflection;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();


    // // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyprojectdbContext>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
