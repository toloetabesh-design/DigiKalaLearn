using DigiKala.Application.BusinessServices;
using DigiKala.Application.Interfaces;
using DigiKala.Application.Mapping;
using DigiKala.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace DigiKala.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ۱. تنظیم Logger برای شروع کار قبل از ساخت Builder
            var logger = LogManager.Setup().GetCurrentClassLogger();

            try
            {
                logger.Information("Starting DigiKala Web API...");

                var builder = WebApplication.CreateBuilder(args);

                // ۲. اتصال NLog به سیستم Logging پیش‌فرض ASP.NET Core
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // --- ثبت سرویس‌ها (Dependency Injection) ---
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // تنظیم دیتابیس SQL Server
                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                // تنظیم AutoMapper
                builder.Services.AddAutoMapper(typeof(ProductProfile));

                // ثبت Repository و Service (مطابق با Clean Architecture شما)
                builder.Services.AddScoped<IProductRepository, ProductRepository>();
                builder.Services.AddScoped<IProductService, ProductService>();
                // ------------------------------------------

                var app = builder.Build();

                // ۳. تنظیمات Middleware (پایپ‌لاین درخواست‌ها)
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    logger.Information("Swagger is enabled in Development mode.");
                }

                app.UseAuthorization();
                app.MapControllers();

                // ۴. اجرای برنامه
                app.Run();
            }
            catch (Exception exception)
            {
                // اگر در هنگام بالا آمدن برنامه خطایی رخ دهد، حتماً لاگ شود
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // اطمینان از اینکه تمام لاگ‌ها قبل از بسته شدن کامل برنامه در فایل ذخیره می‌شوند
                LogManager.Shutdown();
            }
        }
    }
}

