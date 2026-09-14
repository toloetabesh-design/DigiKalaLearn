using DigiKala.Application.Dtos;
using DigiKala.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace DigiKala.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(productService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(productService.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert(ProductDto product)
        {
            productService.Insert(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(ProductDto product)
        {
            productService.Update(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productService.Delete(id);
            return Ok();
        }
    }
}