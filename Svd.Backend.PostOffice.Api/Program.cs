using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Svd.Backend.Application;
using Svd.Backend.Persistence;
using Svd.Backend.PostOffice.Settings;
using Svd.Backend.PostOffice.StartupConfiguration;

CultureInfo.DefaultThreadCurrentCulture =
    CultureInfo.InvariantCulture;

LoggingStartup.ConfigureBootstrapSerilog();
GlobalErrorHandler.HandleErrors();
DotNetEnv.Env.Load();

var settings = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build()
    .GetSection(nameof(AppSettings))
    .Get<AppSettings>(c => c.BindNonPublicProperties = true)
    .Validate<AppSettings>();

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog(settings);

builder.Services.AddCors(options =>
    options.AddPolicy("open", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));

builder.Services.AddSingleton(settings);
builder.Services.AddSingleton(settings.Shipment);

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(settings.Persistence);

// Learn more about configuring Swagger/OpenAPI at:
// https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var info = new OpenApiInfo
    {
        Version = settings.Swagger.Version,
        Title = settings.Swagger.Title,
    };
    options.SwaggerDoc(settings.Swagger.Version, info);
});

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseCors("open");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var version = settings.Swagger.Version;
    var path = $"/swagger/{version}/swagger.json";
    var title = settings.Swagger.Title;

    options.SwaggerEndpoint(path, title);
});

app.UseHttpsRedirection();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SvdDbContext>();
    context.Database.Migrate();
}

app.Run();
