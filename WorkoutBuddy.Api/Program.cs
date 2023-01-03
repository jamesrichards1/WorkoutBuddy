using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WorkoutBuddy.Api.Swagger;
using WorkoutBuddy.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<WorkoutBuddyContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkoutBuddy")));

var authenticationConfig = configuration.GetSection("Auth0");
services.AddApiVersioning(
            options => { options.ReportApiVersions = true; });
services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = authenticationConfig.GetValue<string>("Authority");
        options.Audience = authenticationConfig.GetValue<string>("Audience");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Workout Buddy Documentation";
        options.OAuthClientId(authenticationConfig.GetValue<string>("ClientId"));
        options.OAuthAppName(authenticationConfig.GetValue<string>("AppName"));
        options.OAuthUsePkce();
        options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
        {
            { "audience", authenticationConfig.GetValue<string>("Audience") }
        });
    });
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var context = serviceProvider.GetRequiredService<WorkoutBuddyContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
