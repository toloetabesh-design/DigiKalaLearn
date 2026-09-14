using AutoMapper;
using DigiKala.Application.Dtos;
using DigiKala.Application.Interfaces;
using DigiKala.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigiKala.Application.BusinessServices
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        
       

        public ProductDto Get(int id)
        {

            var product = _repository.GetById(id);

            return _mapper.Map<ProductDto>(product);
        }

        public void Insert(ProductDto product)
        {
            var newProduct = _mapper.Map<Product>(product);

           
            _repository.Insert(newProduct);
        }

        public void Update(ProductDto product)
        {
            var updateProduct = _mapper.Map<Product>(product);

            
            _repository.Update(updateProduct);
        }

        public void Delete(int id)
        {
           
            _repository.Delete(id);
        }

        public IEnumerable<ProductDto> Get()
        {
            
            var products = _repository.Get();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDto GetById(int id)
        {
            
            var product = _repository.GetById(id);

            return _mapper.Map<ProductDto>(product);
        }
    }
}

