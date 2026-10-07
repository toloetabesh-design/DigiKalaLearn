
using DigiKala.Application.BusinessServices;
using DigiKala.Application.Interfaces;
using DigiKala.Application.Mapping;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace DigiKala.Presentation

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddScoped<IProductRepository, DigiKala.Persistence.Repositories.ProductRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            {
                


                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
        }

       
        
        }

    
}


