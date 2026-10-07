using DigiKala.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigiKala.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "شازده کوچولو",
                    Price = 250000
                },

                new Product
                {
                    Id = 2,
                    Name = "کیمیاگر",
                    Price = 300000
                },

                new Product
                {
                    Id = 3,
                    Name = "صد سال تنهایی",
                    Price = 450000
                },

                new Product
                {
                    Id = 4,
                    Name = "غرور و تعصب",
                    Price = 350000
                },

                new Product
                {
                    Id = 5,
                    Name = "ملت عشق",
                    Price = 400000
                },

                new Product
                {
                    Id = 6,
                    Name = "مردی به نام اوه",
                    Price = 380000
                }
            );
        }
    }
}
