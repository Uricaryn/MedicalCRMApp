using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.ProductDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductGetDto>> GetAllProductsAsync();
        Task<ProductGetDto> GetProductByIdAsync(Guid id);
        Task CreateProductAsync(ProductCreateDto productCreateDto);
        Task UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task DeleteProductAsync(Guid id);
    }
}
