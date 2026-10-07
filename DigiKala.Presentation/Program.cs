using DigiKala.Application.BusinessServices;
using DigiKala.Application.Interfaces;
using DigiKala.Application.Mapping;
using DigiKala.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using DigiKala.Persistence;

// ۱. تنظیم اولیه لاگر برای مدیریت خطاهای زمان استارت‌آپ
var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Starting DigiKala Web API Application...");

    var builder = WebApplication.CreateBuilder(args);

    // ۲. پیکربندی NLog برای جایگزینی با لاگرهای پیش‌فرض مایکروسافت
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // ۳. اضافه کردن سرویس‌های اصلی ASP.NET Core
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // ۴. تنظیمات دیتابیس (SQL Server)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // ۵. ثبت سرویس‌های مربوط به Product (طبق ساختار Clean Architecture شما)
    // ثبت Repository
    builder.Services.AddScoped<IProductRepository, ProductRepository>();

    // ثبت Service
    builder.Services.AddScoped<IProductService, ProductService>();

    // ۶. تنظیم AutoMapper (فقط برای پروفایل محصول)
    builder.Services.AddAutoMapper(typeof(ProductProfile));

    var app = builder.Build();

    // ۷. پیکربندی Middlewareها (Pipeline)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.Info("Swagger is enabled in Development mode.");
    }

    // استفاده از HTTPS (اختیاری برای توسعه محلی)
    app.UseHttpsRedirection();

    // اجازه دسترسی طبق قوانین تعریف شده
    app.UseAuthorization();

    // نگاشت کنترلرها
    app.MapControllers();

    // ۸. اجرای برنامه
    app.Run();
}
catch (Exception exception)
{
    // اگر برنامه در حین اجرا یا استارت‌آپ کرش کند، خطا را لاگ می‌کند
    logger.Error(exception, "DigiKala Application terminated unexpectedly!");
    throw;
}
finally
{
    // اطمینان از اینکه تمام لاگ‌ها در فایل ذخیره شده‌اند قبل از بسته شدن کامل برنامه
    LogManager.Shutdown();
}
