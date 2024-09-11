using AutoMapper;
using Medical_CRM_Domain.DTOs.ProductDTOs;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        // GET: api/Product
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost("Create")]
        public async Task CreateProductAsync(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
            {
                throw new ArgumentNullException(nameof(productCreateDto), "Product data is required.");
            }

            var product = _mapper.Map<Product>(productCreateDto);

            try
            {
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Veritabanı ile ilgili özel hataları yakalama
                throw new Exception("Error creating product: A database update error occurred. See inner exception for details.", dbEx);
            }
            catch (Exception ex)
            {
                // Diğer hatalar
                throw new Exception("Error creating product: " + ex.Message, ex);
            }
        }

        // PUT: api/Product/{id}
        [HttpPut("Edit/{id}")]
        public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(productUpdateDto), "Product update data is required.");
            }

            var existingProduct = await _unitOfWork.Products.GetByIdAsync(productUpdateDto.Id);

            // Ürün bulunamazsa hata mesajı gösterilir
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found for update.");
            }

            // Güncelleme işlemi için mapping yapılır
            var product = _mapper.Map(productUpdateDto, existingProduct);

            try
            {
                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product: " + ex.Message);
            }
        }

        // DELETE: api/Product/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
