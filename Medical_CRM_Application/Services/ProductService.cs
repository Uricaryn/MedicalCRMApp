using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.ProductDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;

namespace Medical_CRM_Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductGetDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            // Eğer ürün bulunamazsa boş bir liste döner
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductGetDto>();
            }

            return _mapper.Map<IEnumerable<ProductGetDto>>(products);
        }

        public async Task<ProductGetDto> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            // Eğer ürün bulunamazsa hata fırlatılır
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            return _mapper.Map<ProductGetDto>(product);
        }

        public async Task CreateProductAsync(ProductCreateDto productCreateDto)
        {
            // Giriş doğrulaması
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
            catch (Exception ex)
            {
                throw new Exception("Error creating product: " + ex.Message);
            }
        }

        public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(productUpdateDto), "Product update data is required.");
            }

            var existingProduct = await _unitOfWork.Products.GetByIdAsync(productUpdateDto.Id);

            // Mevcut ürün kontrolü
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found for update.");
            }

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

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found for deletion.");
            }

            try
            {
                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product: " + ex.Message);
            }
        }
    }
}
