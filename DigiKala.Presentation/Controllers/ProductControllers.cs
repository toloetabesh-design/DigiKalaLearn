using DigiKala.Application.Dtos;
using DigiKala.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // ۱. اضافه کردن این فضای نام برای استفاده از ILogger

namespace DigiKala.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger; // ۲. تعریف لاگر اختصاصی برای این کنترلر

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger; // ۳. تزریق لاگر از طریق Constructor
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Fetching all products..."); // لاگ اطلاعاتی
            try
            {
                var products = _productService.Get();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products."); // لاگ خطا
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);
            try
            {
                var product = _productService.GetById(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID: {ProductId} not found.", id);
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product with ID: {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Insert(ProductDto product)
        {
            _logger.LogInformation("Attempting to insert a new product: {@Product}", product);
            try
            {
                _productService.Insert(product);
                _logger.LogInformation("Product inserted successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult Update(ProductDto product)
        {
            _logger.LogInformation("Attempting to update product with ID: {ProductId}", product.Id);
            try
            {
                _productService.Update(product);
                _logger.LogInformation("Product updated successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with ID: {ProductId}", product.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogWarning("Attempting to delete product with ID: {ProductId}", id);
            try
            {
                _productService.Delete(id);
                _logger.LogInformation("Product with ID: {ProductId} deleted successfully.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with ID: {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
