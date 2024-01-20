using newProject.Helpers;
using newProject.Entities;
using Microsoft.EntityFrameworkCore;
using newProject.Services;
using System.Text.Json.Serialization;
using AutoMapper;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


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
builder.Services.AddScoped<JwtUtils>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

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
app.UseAuthentication();

app.MapControllers();

app.Run();
