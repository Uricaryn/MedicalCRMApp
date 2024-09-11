using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Infrastructure.Context;

namespace Medical_CRM_Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await GetAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Product>> GetProductsWithLowStockAsync(int threshold)
        {
            return await GetAllAsync(p => p.QuantityInStock <= threshold);
        }
    }
}
