using DigiKala.Application.Dtos;
using DigiKala.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Application.Interfaces
{
    public interface IProductService
        
    {
        IEnumerable<ProductDto> Get();

        ProductDto GetById(int id);

        void Insert(ProductDto product);

        void Update(ProductDto product);

        void Delete(int id);
    }
}