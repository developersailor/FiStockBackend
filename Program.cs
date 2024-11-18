using System.Text;
using FiStockBackend.Context;
using FiStockBackend.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException(nameof(jwtKey), "JWT Key cannot be null or empty.");
}

// Add services to the container
builder.Services.AddDbContext<StockTrackingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    // API Key Security definition for Swagger
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Please enter your API key",
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// API Key Middleware
app.Use(async (context, next) =>
{
    // Swagger endpoint'lerini atla
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        await next();
        return;
    }

    // API anahtarı kontrolü
    if (!context.Request.Headers.TryGetValue("Authorization", out var extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API anahtarı eksik.");
        return;
    }

    var apiKey = builder.Configuration.GetValue<string>("ApiKey");
    if (!apiKey.Equals(extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Yetkisiz erişim.");
        return;
    }

    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FiStockBackend v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();