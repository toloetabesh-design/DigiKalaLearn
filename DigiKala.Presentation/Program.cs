using DigiKala.Application.BusinessServices;
using DigiKala.Application.Interfaces;
using DigiKala.Application.Mapping;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using DigiKala.Infrastructure.Repositories;
using DigiKala.Infrastructure.Data;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Starting DigiKala Web API Application...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IProductRepository, ProductRepository>();


    builder.Services.AddScoped<IProductService, ProductService>();

    builder.Services.AddAutoMapper(typeof(ProductProfile));

    var app = builder.Build();

    // ۷. پیکربندی Middlewareها (Pipeline)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.Info("Swagger is enabled in Development mode.");
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "DigiKala Application terminated unexpectedly!");
    throw;
}
finally
{
    LogManager.Shutdown();
}
