using DigiKala.Application.Interfaces;
using DigiKala.Domain.Entities;
using DigiKala.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DigiKala.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList(); // استفاده از ToList به جای ToListAsync
        }

        public Product? GetById(int id)
        {
            return _context.Products.Find(id); // استفاده از Find به جای FindAsync
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); // استفاده از SaveChanges به جای SaveChangesAsync
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public object Get()
        {
            throw new NotImplementedException();
        }

        public void Insert(Product newProduct)
        {
            throw new NotImplementedException();
        }
    }
}

