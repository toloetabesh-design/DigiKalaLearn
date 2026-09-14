using DigiKala.Domain.Entities;
using System.Collections.Generic;

namespace DigiKala.Application.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get();

        Product GetById(int id);

        void Insert(Product product);

        void Update(Product product);

        void Delete(int id);
        
    }
}